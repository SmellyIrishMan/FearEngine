using FearEngine.Lighting;

namespace FearEngine.Resources.Materials
{
    public interface Material
    {
        string Name{ get;}

        bool IsLoaded();
        void Dispose();
        
        void Apply();

        void SetParameterValue(DefaultMaterialParameters.Param p, SharpDX.Matrix value);
        void SetParameterValue(DefaultMaterialParameters.Param p, SharpDX.Vector3 value);
        void SetParameterValue(DefaultMaterialParameters.Param p, SharpDX.Vector4 value);
        void SetParameterValue(DefaultMaterialParameters.Param p, Light light);

        void SetParameterValue(string p, SharpDX.Matrix value);
        void SetParameterValue(string p, SharpDX.Vector3 value);
        void SetParameterValue(string p, SharpDX.Vector4 value);
        void SetParameterValue(string p, Light light);

        void SetParameterResource(string p, Texture texture);
        void SetParameterResource(string p, SharpDX.Direct3D11.ShaderResourceView shaderResView);
        void SetParameterResource(string p, SharpDX.Direct3D11.UnorderedAccessView unorderedAccView);
        void SetParameterResource(string p, SharpDX.Direct3D11.SamplerState comparisonSampler);
    }
}
