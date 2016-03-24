using FearEngine.Timer;

namespace FearEngine
{
    public interface FearGame
    {
        void Startup(FearEngineImpl engine);

        void Update(GameTimer gameTime);

        void Draw(GameTimer gameTime);

        void Shutdown();
    }
}
