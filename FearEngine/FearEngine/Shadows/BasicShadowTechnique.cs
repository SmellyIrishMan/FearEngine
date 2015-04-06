﻿using FearEngine.DeviceState;
using FearEngine.Lighting;
using FearEngine.RenderTargets;
using FearEngine.Resources;
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
    public class BasicShadowTechnique : ShadowTechnique
    {
        private SharpDX.Toolkit.Graphics.GraphicsDevice device;
        private Material depthMaterial;

        private ShadowMap shadowMap;
        private FearEngine.DeviceState.SamplerStates.SamplerState sampler;

        private Matrix lightTSTrans;    //LightTextureSpaceTransform. Takes us from the world space into somewhere in the [0,1] range plus some depth. Oh baby.
        private Matrix lightVP;

        private RenderTargetStack renderTargetStack;
        private RasteriserState depthRS;

        public Matrix LightTSTrans { get { return lightTSTrans; } }
        public ShaderResourceView ShadowMap { get { return shadowMap.ResourceView; } }
        public FearEngine.DeviceState.SamplerStates.SamplerState ShadowMapSampler { get { return sampler; } }

        public BasicShadowTechnique(FearGraphicsDevice dev,
            [Named("DepthWrite")]Material depthMat, 
            [Named("ShadowBiasedDepth")]RasteriserState depthRasState,
            [Named("ShadowMapComparison")]FearEngine.DeviceState.SamplerStates.SamplerState samp)
        {
            device = dev.Device;
            depthMaterial = depthMat;

            depthRS = depthRasState;

            sampler = samp;
        }

        public void RenderShadowTechnique(MeshRenderer meshRenderer, IEnumerable<SceneObject> shadowedSceneObjects)
        {
            device.SetRasterizerState(depthRS.State);

            renderTargetStack.PushRenderTargetAndSwitch(shadowMap.RenderTarget);
            foreach (SceneObject sceneObj in shadowedSceneObjects)
            {
                depthMaterial.SetParameterValue(DefaultMaterialParameters.Param.WorldViewProj, sceneObj.GameObject.WorldMatrix * lightVP);
                meshRenderer.RenderMeshWithMaterial(sceneObj.Mesh, depthMaterial);
            }
            renderTargetStack.PopRenderTargetAndSwitch();

            device.SetRasterizerState(device.RasterizerStates.Default);
        }
    
        public void SetupForLight(Light light)
        {
            renderTargetStack = new RenderTargetStack(device);

            shadowMap = new ShadowMap(device, 2048, 2048);
            
            const float LIGHT_DISTANCE_FROM_CENTER = 4.0f;

            Vector3 lightPos = Vector3.Zero + (-light.Direction * LIGHT_DISTANCE_FROM_CENTER);

            Matrix View = Matrix.LookAtLH(lightPos, Vector3.Zero, Vector3.Up);
            Matrix Projection = Matrix.OrthoOffCenterLH(-4.0f, 4.0f, -4.0f, 4.0f, -LIGHT_DISTANCE_FROM_CENTER, LIGHT_DISTANCE_FROM_CENTER * 2.5f);

            Matrix NormalisedDeviceCoordinatesToTextureSpaceCoordinates = new Matrix(
                0.5f, 0.0f, 0.0f, 0.0f,
                0.0f, -0.5f, 0.0f, 0.0f,
                0.0f, 0.0f, 1.0f, 0.0f,
                0.5f, 0.5f, 0.0f, 1.0f);

            lightVP = View * Projection;
            lightTSTrans = lightVP * NormalisedDeviceCoordinatesToTextureSpaceCoordinates;
        }

        public void Dispose()
        {
            shadowMap.Dispose();
        }
    }
}
