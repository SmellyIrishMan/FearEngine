using SharpDX.Toolkit;
namespace FearEngine
{
    public interface FearGame
    {
        void Startup(FearEngineImpl engine);

        void Update(GameTime gameTime);

        void Draw(GameTime gameTime);

        void Shutdown();
    }
}
