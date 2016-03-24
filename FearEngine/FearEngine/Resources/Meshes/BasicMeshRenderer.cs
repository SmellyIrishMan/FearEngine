using FearEngine.Resources.Materials;

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
