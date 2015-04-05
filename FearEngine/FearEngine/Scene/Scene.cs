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
        private HashSet<DirectionalLight> lights;

        private HashSet<SceneObject> sceneObjects;

        private HashSet<Mesh> meshes;
        private HashSet<Material> materials;

        private MeshRenderer meshRenderer;
        private Camera camera;
        private Lighting.DirectionalLight defaultLight;

        private bool shadowsEnabled = false;

        public Scene(MeshRenderer meshRend, Camera cam)
        {
            meshRenderer = meshRend;
            camera = cam;

            lights = new HashSet<DirectionalLight>();

            sceneObjects = new HashSet<SceneObject>();
            meshes = new HashSet<Mesh>();
            materials = new HashSet<Material>();

            SetupDefaultLight();
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

            foreach(SceneObject sceneObj in sceneObjects)
            {
                SetupMaterialForGameObject(sceneObj.Material, sceneObj.GameObject);
                meshRenderer.RenderMeshWithMaterial(sceneObj.Mesh, sceneObj.Material);
            }
        }

        private void SetupMaterialsForScene()
        {
            DefaultPerFrameMaterialParameters frameParams = new DefaultPerFrameMaterialParameters(
                camera.Position, defaultLight);

            foreach (Material material in materials)
            {
                frameParams.ApplyToMaterial(material);
            }
        }

        private void SetupMaterialForGameObject(Material material, GameObject obj)
        {
            Matrix world = Matrix.Transformation(Vector3.Zero, Quaternion.Identity, Vector3.One, Vector3.Zero, obj.Transform.Rotation, obj.Transform.Position);

            Matrix view = camera.View;
            Matrix proj = camera.Projection;
            Matrix WVP = world * view * proj;

            DefaultPerObjectMaterialParameters objectParams = new DefaultPerObjectMaterialParameters(
                world,
                Matrix.Transpose(Matrix.Invert(world)),
                WVP);

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
            shadowsEnabled = true;
        }
    }
}
