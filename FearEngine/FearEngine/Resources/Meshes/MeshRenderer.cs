using FearEngine.Cameras;
using FearEngine.GameObjects;
using FearEngine.Lighting;
using SharpDX;
using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources.Meshes
{
    public class MeshRenderer
    {
        private GraphicsDevice device;

        public MeshRenderer(GraphicsDevice dev)
        {
            device = dev;
        }

        public void RenderMeshWithMaterial(Mesh mesh, Material material)
        {
            material.Apply();
            mesh.Render();
        }
    }
}
