using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources.Meshes.Vertex.Layouts
{
    public class PositionNormalTextureLayout : VertexLayout
    {
        static VertexInputLayout inputLayout;
        static int stride;

        public PositionNormalTextureLayout()
        {
            PositionNormalTextureLayout.inputLayout = VertexInputLayout.New(0, typeof(VertexLayouts.PositionNormalTexture));
            PositionNormalTextureLayout.stride = System.Runtime.InteropServices.Marshal.SizeOf(typeof(VertexLayouts.PositionNormalTexture));
        }

        public VertexInputLayout GetInputLayout()
        {
            return inputLayout;
        }

        public int GetStride()
        {
            return stride;
        }
    }
}
