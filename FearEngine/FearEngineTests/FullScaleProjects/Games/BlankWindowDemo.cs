using FearEngine;
using FearEngine.HelperClasses;
using FearEngine.Timer;

namespace FearEngineTests
{
    public class BlankWindowDemo : FearGame
    {
        FearEngineImpl fearEngine;

        private const int MS_TO_RUN_FOR = 10;
        private bool alreadyCalledExit = false;

        public void Startup(FearEngine.FearEngineImpl engine)
        {
            fearEngine = engine;
        }

        public void Update(GameTimer gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds >= MS_TO_RUN_FOR && !alreadyCalledExit)
            {
                alreadyCalledExit = true;
                fearEngine.ExitGame();
            }
        }

        public void Draw(GameTimer gameTime)
        {
            fearEngine.Device.Clear(new SharpDX.Color4(SRGBLinearConverter.SRGBtoLinear(0.2f), 0.0f, SRGBLinearConverter.SRGBtoLinear(0.2f), 1.0f));
        }

        public void Shutdown()
        {
        }
    }
}
