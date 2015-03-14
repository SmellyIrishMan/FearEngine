using FearEngine.Resources.Managment;
using SharpDX.Toolkit.Graphics;
using System;

namespace FearEngine.Resources
{
    public class Material : Resource
    {
        string name;
        Effect effect;

        bool isLoaded = false;

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

        public string GetName()
        {
            return name;
        }

        public Effect GetEffect()
        {
            return effect;
        }
    }
}
