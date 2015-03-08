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

namespace FearEngine
{
    public class FearEngineImpl : Game
    {
        private FearGame game;

        private GraphicsDeviceManager graphicsDeviceManager;

        private FearInput input;
        private FearResourceManager resourceManager;

        private Camera mainCamera;

        private const uint DEFAULT_WIDTH = 1280;
        private const uint DEFAULT_HEIGHT = 720;

        public FearEngineImpl(FearGame gameImpl)
        {
            game = gameImpl;
        }

        public void SetupApp(GraphicsDeviceManager deviceMan, FearResourceManager resMan, FearInput paramInput)
        {
            graphicsDeviceManager = deviceMan;
            SetupDevice();

            resourceManager = resMan;

            input = paramInput;
        }

        private void SetupDevice()
        {
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
            base.Initialize();
            Window.Title = "Fear Engine V1.0";

            FearLog.Initialise();
            resourceManager.Initialize(GetDevice());

            SharpDX.ViewportF viewport = new SharpDX.Viewport(0, 0, (int)DEFAULT_WIDTH, (int)DEFAULT_HEIGHT, 0.0f, 1.0f);
            graphicsDeviceManager.GraphicsDevice.SetViewport(viewport);

            Transform cameraTransform = new Transform();
            cameraTransform.MoveTo(new Vector3(1, 2, -5));
            mainCamera = new Camera("MainCamera", cameraTransform, new CameraControllerComponent(input), GetDevice().GetViewport(0).AspectRatio);

            game.Startup(this);
        }

        protected override void Draw(GameTime gameTime)
        {
            game.Draw(gameTime);

            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            input.Update(gameTime);

            mainCamera.Update(gameTime);

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

        protected override void OnExiting(object sender, EventArgs args)
        {
            game.Shutdown();

            resourceManager.Shutdown();
        }
    }
}
