using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources.Meshes
{
    public class MeshRendererFactory
    {
        public MeshRenderer CreateMeshRenderer(GraphicsDevice device)
        {
            return new MeshRenderer(device);
        }
    }
}
