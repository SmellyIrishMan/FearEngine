using SharpDX;
using SharpDX.Toolkit.Graphics;
namespace FearEngine.Resources.Meshes
{
    public static class VertexLayouts
    {
        public struct PositionColor
        {
            [VertexElement("POSITION")]     //THIS MUST MATCH THE SEMANTIC NAME IN THE SHADER IN ORDER TO GET MATCHED CORRECTLY
            public Vector3 Position;

            [VertexElement("COLOR")]
            public Vector4 Color;
        }

        public struct PositionNormal
        {
            [VertexElement("POSITION")]     //THIS MUST MATCH THE SEMANTIC NAME IN THE SHADER IN ORDER TO GET MATCHED CORRECTLY
            public Vector3 Position;

            [VertexElement("NORMAL")]
            public Vector3 Normal;
        }

        public struct PositionNormalTexture
        {
            [VertexElement("POSITION")]     //THIS MUST MATCH THE SEMANTIC NAME IN THE SHADER IN ORDER TO GET MATCHED CORRECTLY
            public Vector3 Position;

            [VertexElement("NORMAL")]
            public Vector3 Normal;

            [VertexElement("TEXCOORD0")]
            public Vector2 TexCoord;
        }
    }
}
