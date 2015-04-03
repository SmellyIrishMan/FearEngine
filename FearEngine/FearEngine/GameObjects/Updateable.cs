using SharpDX.Toolkit;
namespace FearEngine.GameObjects
{
    public interface Updateable
    {
        void Update(GameObject owner, GameTime gameTime);
    }
}
