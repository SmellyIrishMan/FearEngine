﻿using FearEngine.DeviceState;
using FearEngine.RenderTargets;
using FearEngine.Resources;
using FearEngine.Resources.Materials;
using FearEngine.Resources.Meshes;
using FearEngine.Scene;
using SharpDX;
using SharpDX.Direct3D11;
using System.Collections.Generic;

namespace FearEngine.Shadows
{
    public class BasicShadowTechnique
    {
        private RenderTargetStack renderTargetStack;

        private ShadowMap shadowMap;
        private SharpDX.Toolkit.Graphics.GraphicsDevice device;

        private Material depthMaterial;

        private Matrix lightTSTrans;    //LightTextureSpaceTransform. Takes us from the world space into somewhere in the [0,1] range plus some depth. Oh baby.
        private Matrix lightVP;

        private SharpDX.Toolkit.Graphics.RasterizerState depthRS;
        private SharpDX.Toolkit.Graphics.RasterizerState normRS;

        public Matrix LightTSTrans { get { return lightTSTrans; } }
        public ShaderResourceView ShadowMap { get { return shadowMap.ResourceView; } }

        public BasicShadowTechnique(SharpDX.Toolkit.Graphics.GraphicsDevice dev, Material depthMat, DeviceStateFactory devStateFac)
        {
            device = dev;
            depthMaterial = depthMat;

            depthRS = devStateFac.CreateRasteriserState(DeviceStateFactory.RasterizerStates.ShadowBiasedDepth);
            normRS = devStateFac.CreateRasteriserState(DeviceStateFactory.RasterizerStates.Default);
        }

        public void RenderShadowTechnique(MeshRenderer meshRenderer, IEnumerable<SceneObject> shadowedSceneObjects)
        {
            device.SetRasterizerState(depthRS);
            renderTargetStack.PushRenderTargetAndSwitch(shadowMap.RenderTarget);

            foreach (SceneObject sceneObj in shadowedSceneObjects)
            {
                depthMaterial.SetParameterValue(DefaultMaterialParameters.Param.WorldViewProj, sceneObj.GameObject.WorldMatrix * lightVP);
                meshRenderer.RenderMeshWithMaterial(sceneObj.Mesh, depthMaterial);
            }
            renderTargetStack.PopRenderTargetAndSwitch();
            device.SetRasterizerState(normRS);
        }
    
        public void SetupForLight(Lighting.DirectionalLight light)
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