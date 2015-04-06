using SharpDX.DXGI;
using System;
using FearEngine.Cameras;
using FearEngine.Resources;
using FearEngine.Logger;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Input;
using SharpDX.Direct3D11;
using SharpDX.Toolkit;
using FearEngine.Resources.Managment;
using SharpDX;
using FearEngine.Input;
using FearEngine.GameObjects;
using System.Collections.Generic;
using FearEngine.HelperClasses;

namespace FearEngine
{
    public delegate void EngineInitialisedHandler(FearEngineImpl engine);

    public class FearEngineImpl : Game
    {
        private FearGame game;
        private string title;

        private GraphicsDeviceManager graphicsDeviceManager;

        private FearInput input;
        private FearResourceManager resourceManager;

        private List<GameObject> gameObjects;   //This gameObjects list should be taken somewhere else. Like some factory that creates gameObjects, tracks them, updates them.
        private Camera mainCamera;

        private const uint DEFAULT_WIDTH = 1280;
        private const uint DEFAULT_HEIGHT = 720;

        public event EngineInitialisedHandler Initialised;

        public FearEngineImpl(FearGame gameImpl)
        {
            title = "Fear Engine";

            game = gameImpl;
            gameObjects = new List<GameObject>();

            SharpDX.Configuration.EnableObjectTracking = true;
        }

        public FearEngineImpl(FearGame gameImpl, string t) : this(gameImpl)
        {
            title = t;
        }

        public void SetupDeviceManager(GraphicsDeviceManager deviceMan)
        {
            graphicsDeviceManager = deviceMan;
            graphicsDeviceManager.DeviceCreationFlags = DeviceCreationFlags.Debug;

            graphicsDeviceManager.PreferredBackBufferFormat = Format.R8G8B8A8_UNorm_SRgb;
            graphicsDeviceManager.PreferredBackBufferWidth = (int)DEFAULT_WIDTH;
            graphicsDeviceManager.PreferredBackBufferHeight = (int)DEFAULT_HEIGHT;
        }

        internal void InjectDependencies(FearResourceManager resMan, FearInput fearInput)
        {
            resourceManager = resMan;
            input = fearInput;
        }

        protected override void OnActivated(object sender, EventArgs args)
        {
            base.OnActivated(sender, args);
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
        }

        protected override void Initialize()
        {
            Window.Title = title;

            base.Initialize();

            if (Initialised != null)
            {
                Initialised(this);
            }

            FearLog.Initialise();

            SharpDX.ViewportF viewport = new SharpDX.Viewport(0, 0, (int)DEFAULT_WIDTH, (int)DEFAULT_HEIGHT, 0.0f, 1.0f);
            graphicsDeviceManager.GraphicsDevice.SetViewport(viewport);

            Transform cameraTransform = new Transform();
            cameraTransform.MoveTo(new Vector3(1, 2, -5));

            GameObject cameraObject = new GameObject("MainCamera", cameraTransform);
            cameraObject.AddUpdatable(new CameraControllerComponent(input));
            gameObjects.Add(cameraObject);

            mainCamera = new Camera(cameraObject, GetDevice().GetViewport(0).AspectRatio);

            game.Startup(this);
        }

        protected override void Draw(GameTime gameTime)
        {
            GetDevice().Clear(new SharpDX.Color4(SRGBLinearConverter.SRGBtoLinear(0.2f), 0.0f, SRGBLinearConverter.SRGBtoLinear(0.2f), 1.0f));

            game.Draw(gameTime);

            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            input.Update(gameTime);

            foreach (GameObject gameObj in gameObjects)
            {
                gameObj.Update(gameTime);
            }

            base.Update(gameTime);

            game.Update(gameTime);
        }

        public GraphicsDevice GetDevice()
        {
            return graphicsDeviceManager.GraphicsDevice;
        }

        public DeviceContext GetContext()
        {
            return (DeviceContext) graphicsDeviceManager.GraphicsDevice;
        }

        public FearResourceManager GetResourceManager()
        {
            return resourceManager;
        }

        public Camera GetMainCamera()
        {
            return mainCamera;
        }

        public void ExitGame()
        {
            this.Exit();
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            game.Shutdown();

            resourceManager.Shutdown();

            if (SharpDX.Diagnostics.ObjectTracker.FindActiveObjects().Count > 0)
            {
                throw new UnleasedObjectsException();
            }
        }

        public class UnleasedObjectsException : Exception
        {
            public UnleasedObjectsException()
            {
            }

            public UnleasedObjectsException(string message)
                : base(message)
            {
            }

            public UnleasedObjectsException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
    }
}
