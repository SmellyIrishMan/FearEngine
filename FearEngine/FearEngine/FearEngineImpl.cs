using SharpDX.DXGI;
using System;
using FearEngine.Cameras;
using FearEngine.Logger;
using SharpDX.Toolkit.Graphics;
using SharpDX.Direct3D11;
using SharpDX.Toolkit;
using FearEngine.Inputs;
using FearEngine.GameObjects;
using FearEngine.HelperClasses;
using FearEngine.Scenes;
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
        private LightFactory lightFactory;
        private CameraFactory cameraFactory;

        public GraphicsDevice Device { get {return graphicsDeviceManager.GraphicsDevice; } }
        public DeviceContext Context { get {return (DeviceContext)graphicsDeviceManager.GraphicsDevice;} }
        public FearResourceManager Resources { get { return resourceManager; } }
        public GameObjectFactory GameObjectFactory { get { return gameObjectFactory; } }
        public UpdateableFactory UpdateableFactory { get { return updateableFactory; } }
        public SceneFactory SceneFactory { get { return sceneFactory; } }
        public LightFactory LightFactory { get { return lightFactory; } }
        public CameraFactory CameraFactory { get { return cameraFactory; } }

        private const uint DEFAULT_WIDTH = 1280;
        private const uint DEFAULT_HEIGHT = 720;

        public event EngineInitialisedHandler Initialised;

        public FearEngineImpl(FearGame gameImpl)
        {
            title = "Fear Engine";

            game = gameImpl;

            SharpDX.Configuration.EnableObjectTracking = true;
        }

        public FearEngineImpl(FearGame gameImpl, string t)
            : this(gameImpl)
        {
            title = t;
        }

        internal void InjectDependencies(FearResourceManager resMan,
            Input fearInput,
            GameObjectFactory gameObjFactory,
            UpdateableFactory updateableFac,
            SceneFactory sceneFac,
            LightFactory lightFac,
            CameraFactory cameraFac)
        {
            resourceManager = resMan;
            input = fearInput;
            gameObjectFactory = gameObjFactory;
            updateableFactory = updateableFac;
            sceneFactory = sceneFac;
            lightFactory = lightFac;
            cameraFactory = cameraFac;
        }

        public void SetupDeviceManager(GraphicsDeviceManager deviceMan)
        {
            graphicsDeviceManager = deviceMan;
            graphicsDeviceManager.DeviceCreationFlags = DeviceCreationFlags.Debug;

            graphicsDeviceManager.PreferredBackBufferFormat = Format.R8G8B8A8_UNorm_SRgb;
            graphicsDeviceManager.PreferredBackBufferWidth = (int)DEFAULT_WIDTH;
            graphicsDeviceManager.PreferredBackBufferHeight = (int)DEFAULT_HEIGHT;
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

            game.Startup(this);
        }

        protected override void Draw(GameTime gameTime)
        {
            Device.Clear(new SharpDX.Color4(SRGBLinearConverter.SRGBtoLinear(0.2f), 0.0f, SRGBLinearConverter.SRGBtoLinear(0.2f), 1.0f));

            game.Draw(new FearGameTimer(gameTime));

            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            input.Update(gameTime);

            base.Update(gameTime);

            game.Update(new FearGameTimer(gameTime));
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
