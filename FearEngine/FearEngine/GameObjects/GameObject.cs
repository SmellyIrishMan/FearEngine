using FearEngine.Timer;
using SharpDX;

namespace FearEngine.GameObjects
{
    public interface GameObject
    {
        ulong ID { get; }
        string Name { get;  }
        Transform Transform { get;  }
        Matrix WorldMatrix{ get; }

        void Update(GameTimer gameTime);
        void AddUpdatable(Updateable updater);
    }
}
