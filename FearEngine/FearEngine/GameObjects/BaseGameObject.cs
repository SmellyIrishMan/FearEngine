﻿using FearEngine.Timer;
using SharpDX;
using System.Collections.Generic;
namespace FearEngine.GameObjects
{
    public class BaseGameObject : GameObject
    {
        private static ulong UIDGenerator = 1;

        public ulong ID { get; private set; }
        public string Name { get; private set; }
        public Transform Transform { get; private set; }
        public Matrix WorldMatrix { get  {
            return Matrix.Transformation(Vector3.Zero, Quaternion.Identity, Transform.Scale, Vector3.Zero, Transform.Rotation, Transform.Position); } 
        }

        private List<Updateable> updaters;

        public BaseGameObject(string name)
        {
            AssignUniqueID();

            Name = name;
            Transform = new Transform();

            updaters = new List<Updateable>();
        }

        public BaseGameObject(string name, Transform transform) 
            : this(name)
        {
            Transform = transform;
        }

        public void Update(GameTimer gameTime)
        {
            foreach (Updateable updater in updaters)
            {
                updater.Update(gameTime);
            }
        }

        private void AssignUniqueID()
        {
            ID = UIDGenerator;
            UIDGenerator++;
        }

        public void AddUpdatable(Updateable updater)
        {
            updaters.Add(updater);
        }
    }
}
