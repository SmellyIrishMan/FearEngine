using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources.Meshes
{
    public class PositionNormalTextureLayout
    {
        static VertexInputLayout inputLayout;
        static int stride;

        public PositionNormalTextureLayout()
        {
            PositionNormalTextureLayout.inputLayout = VertexInputLayout.New(0, typeof(VertexLayouts.PositionNormalTexture));
            PositionNormalTextureLayout.stride = System.Runtime.InteropServices.Marshal.SizeOf(typeof(VertexLayouts.PositionNormalTexture));
        }

        public VertexLayouts.PositionNormalTexture[] GetVertices(VertexData[] data, uint count)
        {
            VertexLayouts.PositionNormalTexture[] vertexData = new VertexLayouts.PositionNormalTexture[count];
            for (int i = 0; i < count; i++)
            {
                vertexData[i].Position = data[i].GetValue(VertexInfoType.POSITION);
                vertexData[i].Normal = data[i].GetValue(VertexInfoType.NORMAL);
                vertexData[i].TexCoord = ConvertVec3ToVec2ByDroppingZ(data[i].GetValue(VertexInfoType.TEXCOORD1));
            }

            return vertexData;
        }

        private SharpDX.Vector2 ConvertVec3ToVec2ByDroppingZ(SharpDX.Vector3 vector3)
        {
            return new SharpDX.Vector2(vector3.X, vector3.Y);
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
