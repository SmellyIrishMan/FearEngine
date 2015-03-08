namespace FearEngine
{
    public interface FearGame
    {
        void Startup(FearEngineImpl engine);

        void Update(FearGameTime gameTime);

        void Draw(FearGameTime gameTime);

        void Shutdown();
    }
}
