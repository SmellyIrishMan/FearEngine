using FearEngine.Input;
using FearEngine.Resources.Managment;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Input;

namespace FearEngine
{
    public class FearAppFactory
    {
        public FearEngineApp CreateFearApp(FearEngineApp app)
        {
            GraphicsDeviceManager graphicsMan = new GraphicsDeviceManager(app);

            FearResourceManager resMan = new FearResourceManager(graphicsMan.GraphicsDevice);

            app.SetupApp(graphicsMan, resMan, new FearInput(new MouseManager(app), new KeyboardManager(app)));

            return app;
        }
    }
}
