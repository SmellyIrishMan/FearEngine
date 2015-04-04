﻿using FearEngine.Lighting;
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
            effect.Dispose();
        }

        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        public void Apply()
        {
            effect.CurrentTechnique.Passes[0].Apply();
        }

        public void SetParameterValue(string p, SharpDX.Matrix value)
        {
            if (HasParameter(p))
            {
                effect.Parameters[p].SetValue(value);
            }
        }

        internal void SetParameterValue(string p, SharpDX.Vector3 value)
        {
            if (HasParameter(p))
            {
                effect.Parameters[p].SetValue(value);
            }
        }

        internal void SetParameterValue(string p, SharpDX.Vector4 value)
        {
            if (HasParameter(p))
            {
                effect.Parameters[p].SetValue(value);
            }
        }

        public void SetParameterValue(string p, FearEngine.Lighting.DirectionalLight testLight)
        {
            if (HasParameter(p))
            {
                effect.Parameters[p].SetValue<FearEngine.Lighting.DirectionalLight>(testLight);
            }
        }

        public void SetParameterResource(string p, Texture texture)
        {
            if (HasParameter(p))
            {
                effect.Parameters[p].SetResource(texture.ShaderView);
            }
        }

        private bool HasParameter(string param)
        {
            return effect.Parameters[param] != null;
        }

    }
}