using SharpDX.Toolkit;
namespace FearEngine
{
    public class GameObject
    {
        private static ulong UIDGenerator = 1;

        public ulong ID { get; private set; }
        public string Name { get; private set; }
        public Transform Transform { get; private set; }

        public GameObject(string name)
        {
            AssignUniqueID();

            Name = name;
            Transform = new Transform();
        }

        public GameObject(string name, Transform transform) 
            : this(name)
        {
            Transform = transform;
        }

        private void AssignUniqueID()
        {
            ID = UIDGenerator;
            UIDGenerator++;
        }

        public virtual void Update(GameTime gameTime){}
    }
}
