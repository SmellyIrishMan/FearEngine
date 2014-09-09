using FearEngine.Components;
using SharpDX.Toolkit;
using System.Collections.Generic;

namespace FearEngine.Systems
{
    class FearSystem
    {
        protected bool mEnabled;
        protected List<GameObject> Objects;
        
        public FearSystem()
        {

        }

        public void AddObject(GameObject obj)
        {
            Objects.Add(obj);
        }

        public void RemoveObject(GameObject obj)
        {
            Objects.Remove(obj);
        }

        virtual public void UpdateObjects(GameTime gameTime){}

        virtual public void DrawObjects(GameTime gameTime) {}
    }
}
