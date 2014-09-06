using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace FearEngine.Resources.Meshes
{
    public class Mesh
    {
        private uint vertexCount;
        private uint indexCount;

        private Buffer vertexBuffer;
        private Buffer indexBuffer;

        public Mesh(VertexLayouts.PositionNormalTexture[] vertices, UInt32[] indices)
        {
            vertexCount = (uint)vertices.Length;
            BufferDescription vertDesc = new BufferDescription();
            vertDesc.Usage = ResourceUsage.Default;
            vertDesc.SizeInBytes = VertexLayouts.PositionNormalTexture.GetByteSize() * vertices.Length;
            vertDesc.BindFlags = BindFlags.VertexBuffer;
            vertDesc.CpuAccessFlags = CpuAccessFlags.None;
            vertDesc.OptionFlags = ResourceOptionFlags.None;
            vertDesc.StructureByteStride = 0;
            //vertexBuffer = Buffer.Create(FearEngineApp.Device, vertices, vertDesc);

            indexCount = (uint)indices.Length;
            BufferDescription indexDesc = new BufferDescription();
            indexDesc.Usage = ResourceUsage.Default;
            indexDesc.SizeInBytes = SharpDX.Utilities.SizeOf<UInt32>() * indices.Length;
            indexDesc.BindFlags = BindFlags.IndexBuffer;
            indexDesc.CpuAccessFlags = CpuAccessFlags.None;
            indexDesc.OptionFlags = ResourceOptionFlags.None;
            indexDesc.StructureByteStride = 0;
            //indexBuffer = Buffer.Create(FearEngineApp.Device, indices, indexDesc);
        }

        public Buffer GetVertexBuffer()
        {
            return vertexBuffer;
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
