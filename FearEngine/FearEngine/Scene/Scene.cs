using FearEngine.Cameras;
using FearEngine.DeviceState;
using FearEngine.GameObjects;
using FearEngine.Lighting;
using FearEngine.RenderTargets;
using FearEngine.Resources;
using FearEngine.Resources.Materials;
using FearEngine.Resources.Meshes;
using FearEngine.Shadows;
using FearEngine.Techniques;
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

        private LightFactory lightFactory;
        private Lighting.DirectionalLight defaultLight;

        private TechniqueFactory techniqueFactory;

        private bool shadowsEnabled = false;
        private BasicShadowTechnique shadowTech;

        public Scene(MeshRenderer meshRend, Camera cam, LightFactory lightFac, TechniqueFactory techFac)
        {
            meshRenderer = meshRend;
            camera = cam;

            lightFactory = lightFac;
            techniqueFactory = techFac;

            lights = new HashSet<DirectionalLight>();

            sceneObjects = new HashSet<SceneObject>();
            meshes = new HashSet<Mesh>();
            materials = new HashSet<Material>();

            SetupDefaultLight();
        }

        private void SetupDefaultLight()
        {
            defaultLight = lightFactory.CreateLight();
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
            foreach (Material material in materials)
            {
                material.SetParameterValue(DefaultMaterialParameters.Param.EyeW, camera.Position);
                material.SetParameterValue(DefaultMaterialParameters.Param.DirLight, defaultLight);

                if (shadowsEnabled)
                {
                    material.SetParameterResource("gShadowMap", shadowTech.ShadowMap);
                    material.SetParameterResource("gShadowSampler", shadowTech.ShadowMapSampler);
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
            shadowsEnabled = true;
            shadowTech = techniqueFactory.CreateShadowTechnique(ShadowTechnique.Basic);
            shadowTech.SetupForLight(defaultLight);
        }
    }
}
