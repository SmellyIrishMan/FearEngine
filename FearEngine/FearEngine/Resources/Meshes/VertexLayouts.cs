using SharpDX;
using SharpDX.Toolkit.Graphics;
namespace FearEngine.Resources.Meshes
{
    public static class VertexLayouts
    {
        public struct PositionColor
        {
            [VertexElement("SV_Position")]
            public Vector3 Position;

            [VertexElement("COLOR")]
            public Vector4 Color;
        }

        public struct PositionNormal
        {
            [VertexElement("SV_Position")]
            public Vector3 Position;

            [VertexElement("NORMAL")]
            public Vector3 Normal;
        }

        public struct PositionNormalTexture
        {
            [VertexElement("SV_Position")]
            public Vector3 Position;

            [VertexElement("NORMAL")]
            public Vector3 Normal;

            [VertexElement("TEXCOORD0")]
            public Vector2 TexCoord;
        }
    }
}
