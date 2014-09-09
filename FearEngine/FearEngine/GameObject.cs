using FearEngine.Components;
using SharpDX.Toolkit;
using System.Collections.Generic;
namespace FearEngine
{
    public class GameObject
    {
        private static ulong UIDGenerator = 1;

        public ulong ID { get; private set; }
        public string Name { get; private set; }
        public Transform Transform { get; private set; }

        private List<FearComponent> mComponents;

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

        //TODO IF WE MOVE TO AN ENTITY SYSTEM APPROACH THEN THIS NEEDS TO BE REMOVED. OBJECTS SHOULD ONLY HAVE COMPONENTS.
        virtual public void Update(GameTime gameTime)
        {
        }

        public void AddComponent(FearComponent newComponent)
        {
            mComponents.Add(newComponent);
        }

        public void RemoveComponent(FearComponent componentToRemove)
        {
            mComponents.Remove(componentToRemove);
        }

        private void AssignUniqueID()
        {
            ID = UIDGenerator;
            UIDGenerator++;
        }
    }
}
