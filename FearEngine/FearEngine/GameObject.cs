using SharpDX.Toolkit;
namespace FearEngine
{
    public class GameObject
    {
        public string Name { get; private set; }
        public Transform Transform { get; private set; }

        public GameObject(string name)
        {
            Name = name;
            Transform = new Transform();
        }

        public GameObject(string name, Transform transform) 
            : this(name)
        {
            Transform = transform;
        }

        public virtual void Update(GameTime gameTime){}
    }
}
