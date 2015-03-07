using SharpDX.Toolkit.Graphics;
using System;
using Buffer = SharpDX.Toolkit.Graphics.Buffer;
namespace FearEngine.Resources.Meshes
{
    public class MeshInformation
    {
        private uint vertexCount;
        private uint indexCount;

        VertexLayouts.PositionNormalTexture[] vertices;
        UInt32[] indices;

        public MeshInformation()
        {
            vertexCount = 0;
            indexCount = 0;
        }

        public MeshInformation(VertexLayouts.PositionNormalTexture[] verts, UInt32[] ind)
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
            return vertices;
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
