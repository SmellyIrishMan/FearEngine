using SharpDX.Direct3D11;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buffer = SharpDX.Toolkit.Graphics.Buffer;
namespace FearEngine.Resources.Meshes
{
    public class Mesh
    {
        private uint vertexCount;
        private uint indexCount;

        private Buffer<VertexLayouts.PositionNormalTexture> vertexBuffer;
        private Buffer indexBuffer;

        //TODO This should probably be generalised outside of the mesh.
        VertexInputLayout inputLayout;

        public Mesh(VertexLayouts.PositionNormalTexture[] vertices, UInt32[] indices)
        {
            vertexCount = (uint)vertices.Length;
            vertexBuffer = Buffer.Vertex.New(FearEngineApp.GetDevice(), vertices);

            inputLayout = VertexInputLayout.FromBuffer(0, vertexBuffer);

            indexCount = (uint)indices.Length;
            indexBuffer = Buffer.Index.New(FearEngineApp.GetDevice(), indices);
        }

        public Buffer<VertexLayouts.PositionNormalTexture> GetVertexBuffer()
        {
            return vertexBuffer;
        }

        public VertexInputLayout GetInputLayout()
        {
            return inputLayout;
        }

        public Buffer GetIndexBuffer()
        {
            return indexBuffer;
        }

        public uint GetIndexCount()
        {
            return indexCount;
        }

        public void Dispose()
        {
            vertexBuffer.Dispose();
            indexBuffer.Dispose();
        }
    }
}
