using FearEngine.Lighting;
using FearEngine.Resources.Managment;
using FearEngine.Resources.Materials;
using SharpDX.Direct3D11;
using SharpDX.Toolkit.Graphics;
using System;

namespace FearEngine.Resources
{
    public class Material : FearEngine.Resources.Managment.Resource
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

        public void SetParameterValue(DefaultMaterialParameters.Param p, SharpDX.Matrix value)
        {
            SetParameterValue(DefaultMaterialParameters.ParamToName[p], value);
        }

        public void SetParameterValue(DefaultMaterialParameters.Param p, SharpDX.Vector3 value)
        {
            SetParameterValue(DefaultMaterialParameters.ParamToName[p], value);
        }

        public void SetParameterValue(DefaultMaterialParameters.Param p, SharpDX.Vector4 value)
        {
            SetParameterValue(DefaultMaterialParameters.ParamToName[p], value);
        }

        public void SetParameterValue(DefaultMaterialParameters.Param p, FearEngine.Lighting.DirectionalLight light)
        {
            SetParameterValue(DefaultMaterialParameters.ParamToName[p], light);
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

        public void SetParameterValue(string p, FearEngine.Lighting.DirectionalLight light)
        {
            if (HasParameter(p))
            {
                effect.Parameters[p].SetValue<FearEngine.Lighting.DirectionalLight>(light);
            }
        }

        public void SetParameterResource(string p, Texture texture)
        {
            if (HasParameter(p))
            {
                effect.Parameters[p].SetResource(texture.ShaderView);
            }
        }

        public void SetParameterResource(string p, ShaderResourceView shaderResView)
        {
            if (HasParameter(p))
            {
                effect.Parameters[p].SetResource(shaderResView);
            }
        }

        public void SetParameterResource(string p, SharpDX.Direct3D11.SamplerState comparisonSampler)
        {
            if (HasParameter(p))
            {
                effect.Parameters[p].SetResource(comparisonSampler);
            }
        }

        private bool HasParameter(string param)
        {
            return effect.Parameters[param] != null;
        }
    }
}
