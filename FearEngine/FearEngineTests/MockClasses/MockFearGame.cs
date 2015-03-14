using FearEngine;

namespace FearEngineTests
{
    public class MockFearGame : FearGame
    {
        FearEngineImpl fearEngine;

        private const int MS_TO_RUN_FOR = 10;
        private bool alreadyCalledExit = false;

        public void Startup(FearEngine.FearEngineImpl engine)
        {
            fearEngine = engine;
        }

        public void Update(FearGameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds >= MS_TO_RUN_FOR && !alreadyCalledExit)
            {
                alreadyCalledExit = true;
                fearEngine.ExitGame();
            }
        }

        public void Draw(FearGameTime gameTime)
        {
            fearEngine.GetDevice().Clear(new SharpDX.Color4(0.2f, 0.0f, 0.2f, 1.0f));
        }

        public void Shutdown()
        {
        }
    }
}
