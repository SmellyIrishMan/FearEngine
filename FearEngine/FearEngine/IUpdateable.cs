using SharpDX.Toolkit;

namespace FearEngine
{
    public interface IUpdateable
    {
        void Update(GameObject obj, GameTime gameTime);
    }
}
