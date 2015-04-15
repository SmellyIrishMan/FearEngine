using SharpDX.DXGI;
using System;
using FearEngine.Cameras;
using FearEngine.Logger;
using SharpDX.Toolkit.Graphics;
using SharpDX.Direct3D11;
using SharpDX.Toolkit;
using SharpDX;
using FearEngine.Inputs;
using FearEngine.GameObjects;
using System.Collections.Generic;
using FearEngine.HelperClasses;
using FearEngine.Scenes;
using Ninject;
using FearEngine.Resources.Management;
using FearEngine.GameObjects.Updateables;
using FearEngine.Lighting;
using FearEngine.Timer;

namespace FearEngine
{
    public delegate void EngineInitialisedHandler(FearEngineImpl engine);

    public class FearEngineImpl : Game
    {
        private FearGame game;
        private string title;

        private GraphicsDeviceManager graphicsDeviceManager;

        private Input input;
        private FearResourceManager resourceManager;
        private GameObjectFactory gameObjectFactory;
        private UpdateableFactory updateableFactory;
        private SceneFactory sceneFactory;

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

        public FearEngineImpl(FearGame gameImpl, string t)
            : this(gameImpl)
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

        internal void InjectDependencies(FearResourceManager resMan,
            Input fearInput,
            GameObjectFactory gameObjFactory,
            UpdateableFactory updateableFac,
            Camera cam,
            SceneFactory sceneFac)
        {
            resourceManager = resMan;
            input = fearInput;
            gameObjectFactory = gameObjFactory;
            updateableFactory = updateableFac;
            sceneFactory = sceneFac;

            mainCamera = cam;
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

            GameObject cameraObject = gameObjectFactory.CreateGameObject("MainCamera_FirstPersonController");
            cameraObject.AddUpdatable(updateableFactory.CreateCameraControllerComponent());
            gameObjects.Add(cameraObject);

            Vector3 cameraPos = new Vector3(1, 5, -9);
            cameraObject.Transform.MoveTo(cameraPos);
            cameraObject.Transform.LookAt(Vector3.Zero);

            mainCamera.AdjustProjection(SharpDX.MathUtil.Pi * 0.25f, 1280.0f/720.0f, 0.01f, 1000.0f);

            TransformAttacher transformAttacher = (TransformAttacher)mainCamera;
            transformAttacher.AttactToTransform(cameraObject.Transform);

            game.Startup(this);
        }

        public Scenes.Scene CreateEmptyScene()
        {
            GameObject rotatingLight = gameObjectFactory.CreateGameObject("RotatingLightFixture");
            rotatingLight.AddUpdatable(updateableFactory.CreateContinuousRandomSlerp(0.25f));
            gameObjects.Add(rotatingLight);

            Light light = new FearEngine.Lighting.DirectionalLight();
            TransformAttacher attacher = (TransformAttacher)light;
            attacher.AttactToTransform(rotatingLight.Transform);

            return sceneFactory.CreateScene(mainCamera, light);
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
                gameObj.Update(new FearGameTimer(gameTime));
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

            FearLog.Log("Exit Game");
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            FearLog.Log("On Exiting.");

            game.Shutdown();

            resourceManager.Shutdown();

            //if (SharpDX.Diagnostics.ObjectTracker.FindActiveObjects().Count > 0)
            //{
                //FearLog.Log("Active Objects; " + SharpDX.Diagnostics.ObjectTracker.ReportActiveObjects());
                //throw new UnleasedObjectsException();
            //}
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
