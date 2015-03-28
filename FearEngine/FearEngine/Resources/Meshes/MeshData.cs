using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using Buffer = SharpDX.Toolkit.Graphics.Buffer;
namespace FearEngine.Resources.Meshes
{
    public class MeshData
    {
        private uint vertexCount;
        private uint indexCount;

        VertexData[] vertices;
        UInt32[] indices;

        public List<VertexInfoType> Inputs { get { return vertices[0].Inputs; } }

        public MeshData()
        {
            vertexCount = 0;
            indexCount = 0;
        }

        public MeshData(VertexData[] verts, UInt32[] ind)
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

        public VertexData[] GetVertexData()
        {
            return vertices;
        }

        public override bool Equals(object obj)
        {
            MeshData objB = obj as MeshData;
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
