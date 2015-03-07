using SharpDX.Toolkit;

namespace FearEngine
{
    public interface Updateable
    {
        void Update(GameObject owner, GameTime gameTime);
    }
}
