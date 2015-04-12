using FearEngine.Resources.Materials;

namespace FearEngine.Resources.Meshes
{
    public interface MeshRenderer
    {
        void RenderMeshWithMaterial(Mesh mesh, Material material);
    }
}
