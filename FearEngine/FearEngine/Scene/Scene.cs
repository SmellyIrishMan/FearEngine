using FearEngine.Cameras;
using FearEngine.GameObjects;
using FearEngine.Lighting;
using FearEngine.RenderTargets;
using FearEngine.Resources;
using FearEngine.Resources.Meshes;
using FearEngine.Shadows;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.Toolkit;
using System.Collections.Generic;

namespace FearEngine.Scene
{
    public class Scene
    {
        private HashSet<DirectionalLight> lights;

        private HashSet<SceneObject> sceneObjects;

        private HashSet<Mesh> meshes;
        private HashSet<Material> materials;

        private MeshRenderer meshRenderer;
        private Camera camera;
        private Lighting.DirectionalLight defaultLight;

        private RenderTargetStack renderTargetStack;

        private bool shadowsEnabled = false;
        private ShadowMap shadowMap;
        private SharpDX.Toolkit.Graphics.GraphicsDevice device;
        private Material depthMaterial;
        private Matrix shadowTransform;
        private Matrix shadowVP;

        private SharpDX.Toolkit.Graphics.RasterizerState depthRS;
        private SharpDX.Toolkit.Graphics.RasterizerState normRS;

        public Scene(SharpDX.Toolkit.Graphics.GraphicsDevice dev, Material depthMat, MeshRenderer meshRend, Camera cam)
        {
            device = dev;
            depthMaterial = depthMat;
            meshRenderer = meshRend;
            camera = cam;

            lights = new HashSet<DirectionalLight>();

            sceneObjects = new HashSet<SceneObject>();
            meshes = new HashSet<Mesh>();
            materials = new HashSet<Material>();

            SetupDefaultLight();

            RasterizerStateDescription rsd = new RasterizerStateDescription();
            rsd.CullMode = CullMode.Back;
            rsd.DepthBias = 0;
            rsd.DepthBiasClamp = 0.0f;
            rsd.FillMode = FillMode.Solid;
            rsd.IsAntialiasedLineEnabled = false;
            rsd.IsDepthClipEnabled = true;
            rsd.IsFrontCounterClockwise = false;
            rsd.IsMultisampleEnabled = false;
            rsd.IsScissorEnabled = false;
            rsd.SlopeScaledDepthBias = 0.0f;

            normRS = SharpDX.Toolkit.Graphics.RasterizerState.New(device, rsd);

            rsd.CullMode = CullMode.None;
            rsd.DepthBias = 100000;
            rsd.DepthBiasClamp = 0.0f;
            rsd.SlopeScaledDepthBias = 1.0f;
            depthRS = SharpDX.Toolkit.Graphics.RasterizerState.New(device, rsd);
        }

        private void SetupDefaultLight()
        {
            defaultLight = new FearEngine.Lighting.DirectionalLight();

            defaultLight.Diffuse = new SharpDX.Vector4(0.8f, 0.8f, 0.8f, 0.0f);
            defaultLight.Direction = new SharpDX.Vector3(-0.5f, -1.0f, -0.25f);
            defaultLight.Direction.Normalize();
            defaultLight.pad = 0;
        }

        public void AddSceneObject(SceneObject sceneObj)
        {
            sceneObjects.Add(sceneObj);

            AddMaterial(sceneObj.Material);
            AddMesh(sceneObj.Mesh);
        }

        public void Render(GameTime gameTime)
        {
            SetupMaterialsForScene();

            if (shadowsEnabled)
            {
                RenderShadowPass();
            }

            foreach(SceneObject sceneObj in sceneObjects)
            {
                SetupMaterialForGameObject(sceneObj.Material, sceneObj.GameObject);
                meshRenderer.RenderMeshWithMaterial(sceneObj.Mesh, sceneObj.Material);
            }
        }

        private void RenderShadowPass()
        {
            device.SetRasterizerState(depthRS);

            renderTargetStack.PushRenderTargetAndSwitch(shadowMap.RenderTarget);

            foreach (SceneObject sceneObj in sceneObjects)
            {
                SetupMaterialForGameObject(depthMaterial, sceneObj.GameObject);
                
                meshRenderer.RenderMeshWithMaterial(sceneObj.Mesh, depthMaterial);
            }

            renderTargetStack.PopRenderTargetAndSwitch();

            
            device.SetRasterizerState(normRS);
        }

        private void SetupMaterialsForScene()
        {
            DefaultPerFrameMaterialParameters frameParams = new DefaultPerFrameMaterialParameters( camera.Position, defaultLight );

            foreach (Material material in materials)
            {
                frameParams.ApplyToMaterial(material);
            }
        }

        private void SetupMaterialForGameObject(Material material, GameObject obj)
        {
            Matrix world = Matrix.Transformation(Vector3.Zero, Quaternion.Identity, obj.Transform.Scale, Vector3.Zero, obj.Transform.Rotation, obj.Transform.Position);

            material.SetParameterValue("gShadowTransform", world * shadowTransform);
            material.SetParameterResource("gShadowMap", shadowMap.ResourceView);

            Matrix view = camera.View;
            Matrix proj = camera.Projection;
            Matrix WVP = world * view * proj;
            if (material.Name.CompareTo("DepthWrite") == 0)
            {
                WVP = world * shadowVP;
            }

            DefaultPerObjectMaterialParameters objectParams = new DefaultPerObjectMaterialParameters(
                world,
                Matrix.Transpose(Matrix.Invert(world)),
                WVP);

            Vector4 tester = new Vector4(1.4f, 0.0f, 2.4f, 1.0f);
            Vector4 result = Vector4.Transform(tester, WVP);

            Vector4 tester2 = new Vector4(1.4f, 0.0f, 2.4f, 1.0f);
            Vector4 result2 = Vector4.Transform(tester2, world * shadowVP);

            objectParams.ApplyToMaterial(material);
        }

        private void AddLight( Lighting.DirectionalLight light )
        {
            lights.Add(light);
        }

        private void AddMesh(Mesh renderableMesh)
        {
            if (!meshes.Contains(renderableMesh))
            {
                meshes.Add(renderableMesh);
            }
        }

        private void AddMaterial(Material material)
        {
            materials.Add(material);
        }

        public void EnableShadows()
        {
            renderTargetStack = new RenderTargetStack(device);

            shadowsEnabled = true;
            shadowMap = new ShadowMap(device, 2048, 2048);
            
            const float LIGHT_DISTANCE_FROM_CENTER = 4.0f;
            Vector3 lightPos = Vector3.Zero + (-defaultLight.Direction * LIGHT_DISTANCE_FROM_CENTER);

            Matrix View = Matrix.LookAtLH(lightPos, Vector3.Zero, Vector3.Up);
            Matrix Projection = Matrix.OrthoOffCenterLH(-4.0f, 4.0f, -4.0f, 4.0f, -LIGHT_DISTANCE_FROM_CENTER, LIGHT_DISTANCE_FROM_CENTER * 2.5f);

            Matrix NormalisedDeviceCoordinatesToTextureSpaceCoordinates = new Matrix(
                0.5f, 0.0f, 0.0f, 0.0f,
                0.0f, -0.5f, 0.0f, 0.0f,
                0.0f, 0.0f, 1.0f, 0.0f,
                0.5f, 0.5f, 0.0f, 1.0f);

            shadowVP = View * Projection;
            shadowTransform = View * Projection * NormalisedDeviceCoordinatesToTextureSpaceCoordinates;
        }
    }
}
