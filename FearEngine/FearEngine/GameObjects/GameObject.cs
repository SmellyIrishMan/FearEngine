﻿using FearEngine.GameObjects;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using System.Collections.Generic;
namespace FearEngine.GameObjects
{
    public class GameObject
    {
        private static ulong UIDGenerator = 1;

        public ulong ID { get; private set; }
        public string Name { get; private set; }
        public Transform Transform { get; private set; }

        private List<Updateable> updaters;

        public GameObject(string name)
        {
            AssignUniqueID();

            Name = name;
            Transform = new Transform();

            updaters = new List<Updateable>();
        }

        public GameObject(string name, Transform transform) 
            : this(name)
        {
            Transform = transform;
        }

        public void Update( GameTime gameTime )
        {
            foreach (Updateable updater in updaters)
            {
                updater.Update(this, gameTime);
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