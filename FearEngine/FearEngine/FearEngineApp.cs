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
    public class FearEngineApp : Game
    {
        private static GraphicsDeviceManager graphicsDeviceManager;

        private static MouseManager mouse;
        private static KeyboardManager keyboard;

        public static Camera MainCamera;

        private const uint DEFAULT_WIDTH = 1280;
        private const uint DEFAULT_HEIGHT = 720;

        public FearEngineApp()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
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

            SharpDX.ViewportF viewport = new SharpDX.Viewport(0, 0, (int)DEFAULT_WIDTH, (int)DEFAULT_HEIGHT, 0.0f, 1.0f);
            graphicsDeviceManager.GraphicsDevice.SetViewport(viewport);

            FearLog.Initialise();
            ResourceManager.Initialise();

            mouse = new MouseManager(this);
            keyboard = new KeyboardManager(this);

            Transform cameraTransform = new Transform();
            cameraTransform.MoveTo(new Vector3(1, 2, -5));
            MainCamera = new Camera("MainCamera", cameraTransform, new MouseAndKeyboardInputComponent(mouse, keyboard), new CameraControllerComponent());
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            MainCamera.Update(gameTime);

            base.Update(gameTime);
        }

        public static GraphicsDevice GetDevice()
        {
            return graphicsDeviceManager.GraphicsDevice;
        }

        public static DeviceContext GetContext()
        {
            return (DeviceContext) graphicsDeviceManager.GraphicsDevice;
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            ResourceManager.Shutdown();
        }
    }
}
