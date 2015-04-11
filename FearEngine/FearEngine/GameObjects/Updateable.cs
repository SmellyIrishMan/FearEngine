using FearEngine.Timer;
using SharpDX.Toolkit;

namespace FearEngine.GameObjects
{
    public interface Updateable
    {
        void Update(GameObject owner, GameTimer gameTime);
    }
}
