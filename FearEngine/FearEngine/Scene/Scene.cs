using FearEngine.Cameras;
using FearEngine.GameObjects;
using FearEngine.Lighting;
using FearEngine.Resources;
using FearEngine.Resources.Meshes;
using SharpDX;
using SharpDX.Toolkit;
using System.Collections.Generic;

namespace FearEngine.Scene
{
    public class Scene
    {
        private List<DirectionalLight> lights;

        private HashSet<SceneObject> sceneObjects;

        private HashSet<Mesh> meshes;
        private HashSet<Material> materials;

        private MeshRenderer meshRenderer;
        private Camera camera;
        private Lighting.DirectionalLight defaultLight;

        public Scene(MeshRenderer meshRend, Camera cam)
        {
            meshRenderer = meshRend;
            camera = cam;

            lights = new List<DirectionalLight>();

            sceneObjects = new HashSet<SceneObject>();
            meshes = new HashSet<Mesh>();
            materials = new HashSet<Material>();

            SetupDefaultLight();
        }

        private void SetupDefaultLight()
        {
            defaultLight = new FearEngine.Lighting.DirectionalLight();

            defaultLight.Ambient = new SharpDX.Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            defaultLight.Diffuse = new SharpDX.Vector4(0.8f, 0.8f, 0.8f, 0.0f);
            defaultLight.Specular = new SharpDX.Vector4(0.8f, 0.95f, 0.8f, 0.0f);
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
            foreach(SceneObject sceneObj in sceneObjects)
            {
                SetupMaterialForGameObject(sceneObj.Material, sceneObj.GameObject);
                meshRenderer.RenderMeshWithMaterial(sceneObj.Mesh, sceneObj.Material);
            }
        }

        private void SetupMaterialForGameObject(Material material, GameObject obj)
        {
            Matrix world = Matrix.Transformation(Vector3.Zero, Quaternion.Identity, Vector3.One, Vector3.Zero, obj.Transform.Rotation, obj.Transform.Position);

            Matrix view = camera.View;
            Matrix proj = camera.Projection;
            Matrix WVP = world * view * proj;

            material.SetParameterValue("gWorld", world);
            material.SetParameterValue("gWorldInvTranspose", Matrix.Transpose(Matrix.Invert(world)));
            material.SetParameterValue("gWorldViewProj", WVP);

            material.SetParameterValue("gEyeW", camera.Position);
            material.SetParameterValue("gDirLight", defaultLight);
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
    }
}
