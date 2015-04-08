using SharpDX;
using SharpDX.Toolkit;

namespace FearEngine.GameObjects
{
    public interface GameObject
    {
        ulong ID { get; }
        string Name { get;  }
        Transform Transform { get;  }
        Matrix WorldMatrix{ get; }

        void Update(GameTime gameTime);
        void AddUpdatable(Updateable updater);
    }
}
