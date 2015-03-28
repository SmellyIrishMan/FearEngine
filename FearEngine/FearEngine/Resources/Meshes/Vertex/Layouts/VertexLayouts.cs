using FearEngine.Resources.Meshes.Vertex.Layouts;
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

        public struct PositionNormalTangentTexture
        {
            [VertexElement("POSITION")]     //THIS MUST MATCH THE SEMANTIC NAME IN THE SHADER IN ORDER TO GET MATCHED CORRECTLY
            public Vector3 Position;

            [VertexElement("NORMAL")]
            public Vector3 Normal;

            [VertexElement("TANGENT")]
            public Vector3 Tangent;

            [VertexElement("TEXCOORD0")]
            public Vector2 TexCoord;
        }

        internal static VertexLayout DetermineLayout(MeshData meshData)
        {
            if(meshData.Inputs.Contains(VertexInfoType.POSITION))
            {
                if(meshData.Inputs.Contains(VertexInfoType.NORMAL))
                {
                    if (meshData.Inputs.Contains(VertexInfoType.TEXCOORD1))
                    {
                        if (meshData.Inputs.Contains(VertexInfoType.TANGENT))
                        {
                            return new PositionNormalTangentTextureLayout();
                        }
                        else
                        {
                            return new PositionNormalTextureLayout();
                        }
                    }
                }
            }

            throw new UnableToDetermineMeshLayout();
        }
    }
}
