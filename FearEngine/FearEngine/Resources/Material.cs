using FearEngine.Resources.Managment;
using SharpDX.Toolkit.Graphics;
using System;

namespace FearEngine.Resources
{
    public class Material : Resource
    {
        private string name;
        private Effect effect;

        private bool isLoaded;

        public Material()
        {
            name = "";
            effect = null;

            isLoaded = false;
        }

        public Material(string n, Effect e)
        {
            name = n;
            effect = e;

            isLoaded = true;
        }

        public bool IsLoaded()
        {
            return isLoaded;
        }

        public void Dispose()
        {
            Effect.Dispose();
        }

        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        public Effect Effect
        {
            get { return effect; }
            private set { effect = value; }
        }
    }
}
