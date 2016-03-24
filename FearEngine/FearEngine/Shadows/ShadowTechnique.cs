using FearEngine.Lighting;
using FearEngine.Resources.Meshes;
using FearEngine.Scenes;
using SharpDX;
using SharpDX.Direct3D11;
using System.Collections.Generic;

namespace FearEngine.Shadows
{
    public interface ShadowTechnique
    {
        Matrix LightTSTrans { get; }
        ShaderResourceView ShadowMap { get; }
        FearEngine.DeviceState.SamplerStates.SamplerState ShadowMapSampler { get; }

        void RenderShadowTechnique(MeshRenderer meshRenderer, IEnumerable<SceneObject> shadowedSceneObjects);
        void Setup(Light light);
        void Dispose();
    }
}
