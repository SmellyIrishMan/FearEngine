using FearEngine.GameObjects;
using FearEngine.GameObjects.Updateables;
using FearEngine.Lighting;
using FearEngine.Resources.Management;
using FearEngine.Scenes;
using FearEngine.Timer;
using SharpDX.Toolkit;

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
