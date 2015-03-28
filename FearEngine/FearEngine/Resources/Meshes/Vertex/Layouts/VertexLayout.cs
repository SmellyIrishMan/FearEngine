using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources.Meshes.Vertex.Layouts
{
    public interface VertexLayout
    {
        VertexInputLayout GetInputLayout();

        int GetStride();
    }
}
