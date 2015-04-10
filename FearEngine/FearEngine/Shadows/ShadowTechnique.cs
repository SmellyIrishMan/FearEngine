﻿using FearEngine.DeviceState;
using FearEngine.DeviceState.SamplerStates;
using FearEngine.Lighting;
using FearEngine.Resources.Managment;
using FearEngine.Resources.Materials;
using FearEngine.Resources.Meshes;
using FearEngine.Scene;
using FearEngine.Shadows;
using Ninject;
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
        void SetupForLight(Light light);
        void Dispose();
    }
}