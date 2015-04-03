using FearEngine;
using SharpDX.Toolkit;

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

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds >= MS_TO_RUN_FOR && !alreadyCalledExit)
            {
                alreadyCalledExit = true;
                fearEngine.ExitGame();
            }
        }

        public void Draw(GameTime gameTime)
        {
            fearEngine.GetDevice().Clear(new SharpDX.Color4(0.2f, 0.0f, 0.2f, 1.0f));
        }

        public void Shutdown()
        {
        }
    }
}
