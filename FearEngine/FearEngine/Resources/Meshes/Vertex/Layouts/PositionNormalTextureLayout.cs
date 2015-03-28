using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources.Meshes.Vertex.Layouts
{
    public class PositionNormalTextureLayout : VertexLayout
    {
        static VertexInputLayout inputLayout;
        static int stride;

        public PositionNormalTextureLayout()
        {
            System.Type layoutType = typeof(VertexLayouts.PositionNormalTexture);
            PositionNormalTextureLayout.inputLayout = VertexInputLayout.New(0, layoutType);
            PositionNormalTextureLayout.stride = System.Runtime.InteropServices.Marshal.SizeOf(layoutType);
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
