using FearEngine.Time;
using SharpDX.DXGI;
using SharpDX.Windows;
using System;
using System.Windows.Forms;
using Device = SharpDX.Direct3D11.Device;
using Texture2D = SharpDX.Toolkit.Graphics.Texture2D;
using FearEngine.Cameras;
using System.Threading;
using FearEngine.Resources;
using FearEngine.Logger;
using SharpDX.Toolkit.Graphics;
using SharpDX.Direct3D11;
using SharpDX.Toolkit;

namespace FearEngine
{
    public class FearEngineApp : Game
    {
        private static GraphicsDeviceManager graphicsDeviceManager;

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

            FearLog.Initialise();
            ResourceManager.Initialise();
            TimeKeeper.Initialise();

            //InputManager.Initialise(m_Form);
            //InputManager.KeyUp += OnKeyUp;

            MainCamera = new Camera();
        }

        protected override void Draw(GameTime gameTime)
        {
            //InputManager.Update();
            //MainCamera.Update();
            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            //InputManager.Update();
            //MainCamera.Update();
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
