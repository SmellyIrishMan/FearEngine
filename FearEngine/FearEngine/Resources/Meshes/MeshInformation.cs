using SharpDX.Toolkit.Graphics;
using System;
using Buffer = SharpDX.Toolkit.Graphics.Buffer;
namespace FearEngine.Resources.Meshes
{
    public class MeshInformation
    {
        private uint vertexCount;
        private uint indexCount;

        VertexInformation[] vertices;
        UInt32[] indices;

        public MeshInformation()
        {
            vertexCount = 0;
            indexCount = 0;
        }

        public MeshInformation(VertexInformation[] verts, UInt32[] ind)
        {
            vertices = verts;
            indices = ind;

            vertexCount = (uint)vertices.Length;
            indexCount = (uint)indices.Length;
        }

        public uint GetIndexCount()
        {
            return indexCount;
        }

        public uint GetVertexCount()
        {
            return vertexCount;
        }

        public UInt32[] GetIndices()
        {
            return indices;
        }

        public VertexLayouts.PositionNormalTexture[] GetVertices()
        {
           return CreateVertexStructureFromVertexInfo();
        }

        private VertexLayouts.PositionNormalTexture[] CreateVertexStructureFromVertexInfo()
        {
            VertexLayouts.PositionNormalTexture[] vertexData = new VertexLayouts.PositionNormalTexture[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                vertexData[i].Position = vertices[i].GetValue(VertexInfoType.POSITION);
                vertexData[i].Normal = vertices[i].GetValue(VertexInfoType.NORMAL);
                vertexData[i].TexCoord = ConvertVec3ToVec2ByDroppingZ(vertices[i].GetValue(VertexInfoType.TEXCOORD1));
            }

            return vertexData;
        }

        private SharpDX.Vector2 ConvertVec3ToVec2ByDroppingZ(SharpDX.Vector3 vector3)
        {
            return new SharpDX.Vector2(vector3.X, vector3.Y);
        }

        public override bool Equals(object obj)
        {
            MeshInformation objB = obj as MeshInformation;
            if((System.Object)objB != null)
            {
                if (vertexCount == objB.vertexCount && indexCount == objB.indexCount)
                {
                    if (vertexCount == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (int)vertexCount ^ (int)indexCount;
        }
    }
}
