using FearEngine.Input;
using FearEngine.Resources.Managment;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Input;

namespace FearEngine
{
    public class FearGameFactory
    {
        public FearEngineImpl CreateFearGame(FearGame game)
        {
            FearEngineImpl engine = new FearEngineImpl(game);

            GraphicsDeviceManager graphicsMan = new GraphicsDeviceManager(engine);

            FearResourceManager resMan = new FearResourceManager(graphicsMan.GraphicsDevice);

            engine.SetupApp(graphicsMan, resMan, new FearInput(new MouseManager(engine), new KeyboardManager(engine)));

            engine.Run();

            return engine;
        }
    }
}