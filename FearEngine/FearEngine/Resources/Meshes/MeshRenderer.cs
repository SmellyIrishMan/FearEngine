using FearEngine.Cameras;
using FearEngine.GameObjects;
using FearEngine.Lighting;
using SharpDX;
using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources.Meshes
{
    public class MeshRenderer
    {
        public MeshRenderer(GraphicsDevice dev)
        {
        }

        public void RenderMeshWithMaterial(Mesh mesh, Material material)
        {
            material.Apply();
            mesh.Render();
        }
    }
}
