using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources.Meshes.Vertex.Layouts
{
    public class PositionNormalTangentTextureLayout : VertexLayout
    {
        static VertexInputLayout inputLayout;
        static int stride;

        public PositionNormalTangentTextureLayout()
        {
            System.Type layoutType = typeof(VertexLayouts.PositionNormalTangentTexture);
            PositionNormalTangentTextureLayout.inputLayout = VertexInputLayout.New(0, layoutType);
            PositionNormalTangentTextureLayout.stride = System.Runtime.InteropServices.Marshal.SizeOf(layoutType);
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
