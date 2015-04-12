using FearEngine.Cameras;
using FearEngine.GameObjects;
using FearEngine.Lighting;
using FearEngine.Resources.Materials;
using SharpDX;

namespace FearEngine.Resources.Meshes
{
    public class BasicMeshRenderer : MeshRenderer
    {
        public BasicMeshRenderer()
        {
        }

        public void RenderMeshWithMaterial(Mesh mesh, Material material)
        {
            material.Apply();
            mesh.Render();
        }
    }
}
