using FearEngine.Lighting;
using FearEngine.Resources.Managment;
using FearEngine.Resources.Materials;
using SharpDX.Direct3D11;
using SharpDX.Toolkit.Graphics;
using System;
using System.Runtime.InteropServices;

namespace FearEngine.Resources.Materials
{
    public class FearMaterial : FearEngine.Resources.Managment.Resource, FearEngine.Resources.Materials.Material
    {
        private string name;

        //TODO CHANGE THIS BACK TO BEING PRIVATE
        public Effect effect;

        private bool isLoaded;

        public FearMaterial()
        {
            name = "";
            effect = null;

            isLoaded = false;
        }

        public FearMaterial(string n, Effect e)
        {
            name = n;
            effect = e;

            isLoaded = true;
        }

        //TODO GET RID OF THIS
        public FearMaterial(string n, FearResourceManager resMan)
            : this(n, ((FearMaterial)resMan.GetMaterial(n)).effect)
        {
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

        public void SetParameterValue(DefaultMaterialParameters.Param p, Light light)
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

        public void SetParameterValue(string p, SharpDX.Vector3 value)
        {
            if (HasParameter(p))
            {
                effect.Parameters[p].SetValue(value);
            }
        }

        public void SetParameterValue(string p, SharpDX.Vector4 value)
        {
            if (HasParameter(p))
            {
                effect.Parameters[p].SetValue(value);
            }
        }

        public void SetParameterValue(string p, Light light)
        {
            if (HasParameter(p))
            {
                effect.Parameters[p].SetRawValue(GetRawDataFromStruct(light.LightData));
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

        private static byte[] GetRawDataFromStruct(ValueType data)
        {
            int size = Marshal.SizeOf(data);
            byte[] rawData = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);

            Marshal.StructureToPtr(data, ptr, true);
            Marshal.Copy(ptr, rawData, 0, size);
            Marshal.FreeHGlobal(ptr);
            return rawData;
        }
    }
}
