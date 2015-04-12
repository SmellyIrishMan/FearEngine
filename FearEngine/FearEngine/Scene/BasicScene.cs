using FearEngine.Cameras;
using FearEngine.GameObjects;
using FearEngine.Lighting;
using FearEngine.Resources.Materials;
using FearEngine.Resources.Meshes;
using FearEngine.Shadows;
using SharpDX;
using SharpDX.Toolkit;
using System.Collections.Generic;

namespace FearEngine.Scenes
{
    public class BasicScene : Scene
    {
        private HashSet<Light> lights;

        private HashSet<SceneObject> sceneObjects;

        private HashSet<Mesh> meshes;
        private HashSet<Material> materials;

        private BasicMeshRenderer meshRenderer;
        private Camera camera;

        private Lighting.Light defaultLight;

        private bool shadowsEnabled = false;
        private ShadowTechnique shadowTech;

        public BasicScene(BasicMeshRenderer meshRend, Camera cam, Light light, ShadowTechnique shadTech)
        {
            meshRenderer = meshRend;
            camera = cam;

            defaultLight = light;
            shadowTech = shadTech;

            lights = new HashSet<Light>();

            sceneObjects = new HashSet<SceneObject>();
            meshes = new HashSet<Mesh>();
            materials = new HashSet<Material>();
        }

        public void AddSceneObject(SceneObject sceneObj)
        {
            sceneObjects.Add(sceneObj);

            AddMaterial(sceneObj.Material);
            AddMesh(sceneObj.Mesh);
        }

        public void Render(GameTime gameTime)
        {
            if (shadowsEnabled)
            {
                shadowTech.RenderShadowTechnique(meshRenderer, sceneObjects);
            }

            SetupMaterialsForScene();
            foreach(SceneObject sceneObj in sceneObjects)
            {
                SetupMaterialForGameObject(sceneObj.Material, sceneObj.GameObject);
                meshRenderer.RenderMeshWithMaterial(sceneObj.Mesh, sceneObj.Material);
            }
        }

        private void SetupMaterialsForScene()
        {
            foreach (FearMaterial material in materials)
            {
                material.SetParameterValue(DefaultMaterialParameters.Param.EyeW, camera.Position);
                material.SetParameterValue(DefaultMaterialParameters.Param.DirLight, defaultLight);

                if (shadowsEnabled)
                {
                    material.SetParameterResource("gShadowMap", shadowTech.ShadowMap);
                    material.SetParameterResource("gShadowSampler", shadowTech.ShadowMapSampler.State);
                }
            }
        }

        private void SetupMaterialForGameObject(Material material, GameObject obj)
        {
            Matrix world = obj.WorldMatrix;
            Matrix view = camera.View;
            Matrix proj = camera.Projection;
            Matrix WVP = world * view * proj;

            material.SetParameterValue(DefaultMaterialParameters.Param.World, world);
            material.SetParameterValue(DefaultMaterialParameters.Param.WorldInvTranspose, Matrix.Transpose(Matrix.Invert(world)));
            material.SetParameterValue(DefaultMaterialParameters.Param.WorldViewProj, WVP);

            if (shadowsEnabled)
            {
                material.SetParameterValue("gLightTextureSpaceTransform", world * shadowTech.LightTSTrans);
            }
        }

        private void AddLight(Lighting.Light light)
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
            shadowsEnabled = true;
            shadowTech.SetupForLight(defaultLight);
        }
    }
}
