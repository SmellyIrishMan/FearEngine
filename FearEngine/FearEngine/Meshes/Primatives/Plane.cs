using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace FearEngine.Meshes.Primatives
{
    public class Plane
    {
        Point Size { get; set; }  //How many points in the plane? 

        int VertexCount { get; set; }
        int IndexCount { get; set; }
        Buffer VertexBuffer { get; set; }
        Buffer IndexBuffer { get; set; }

        public Plane()
        {
            Size = new Point(100, 100);
            VertexCount = (Size.X - 1) * (Size.Y - 1) * 8;
            IndexCount = VertexCount;

            VertexBuffer = IndexBuffer = null;

            VertexLayouts.PositionColor[] vertices = new VertexLayouts.PositionColor[VertexCount];
            UInt32[] indices = new UInt32[IndexCount];

            UInt32 index = 0;
            float positionX, positionZ;

            for (int j = 0; j < Size.Y - 1; j++)
            {
                for (int i = 0; i < Size.X - 1; i++)
                {
                    // LINE 1
                    // Upper left.
                    positionX = (float)i;
                    positionZ = (float)(j + 1);

                    vertices[index].Position = new Vector3(positionX, 0.0f, positionZ);
                    vertices[index].Color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                    indices[index] = index;
                    index++;

                    // Upper right.
                    positionX = (float)(i + 1);
                    positionZ = (float)(j + 1);

                    vertices[index].Position = new Vector3(positionX, 0.0f, positionZ);
                    vertices[index].Color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                    indices[index] = index;
                    index++;

                    // LINE 2
                    // Upper right.
                    positionX = (float)(i + 1);
                    positionZ = (float)(j + 1);

                    vertices[index].Position = new Vector3(positionX, 0.0f, positionZ);
                    vertices[index].Color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                    indices[index] = index;
                    index++;

                    // Bottom right.
                    positionX = (float)(i + 1);
                    positionZ = (float)j;

                    vertices[index].Position = new Vector3(positionX, 0.0f, positionZ);
                    vertices[index].Color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                    indices[index] = index;
                    index++;

                    // LINE 3
                    // Bottom right.
                    positionX = (float)(i + 1);
                    positionZ = (float)j;

                    vertices[index].Position = new Vector3(positionX, 0.0f, positionZ);
                    vertices[index].Color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                    indices[index] = index;
                    index++;

                    // Bottom left.
                    positionX = (float)i;
                    positionZ = (float)j;

                    vertices[index].Position = new Vector3(positionX, 0.0f, positionZ);
                    vertices[index].Color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                    indices[index] = index;
                    index++;

                    // LINE 4
                    // Bottom left.
                    positionX = (float)i;
                    positionZ = (float)j;

                    vertices[index].Position = new Vector3(positionX, 0.0f, positionZ);
                    vertices[index].Color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                    indices[index] = index;
                    index++;

                    // Upper left.
                    positionX = (float)i;
                    positionZ = (float)(j + 1);

                    vertices[index].Position = new Vector3(positionX, 0.0f, positionZ);
                    vertices[index].Color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                    indices[index] = index;
                    index++;
                }
            }

            BufferDescription vertDesc = new BufferDescription();
            vertDesc.Usage = ResourceUsage.Default;
            vertDesc.SizeInBytes = VertexLayouts.PositionColor.GetByteSize() * VertexCount;
            vertDesc.BindFlags = BindFlags.VertexBuffer;
            vertDesc.CpuAccessFlags = CpuAccessFlags.None;
            vertDesc.OptionFlags = ResourceOptionFlags.None;
            vertDesc.StructureByteStride = 0;
            VertexBuffer = Buffer.Create(FearEngineApp.Device, vertices, vertDesc);

            BufferDescription indexDesc = new BufferDescription();
            indexDesc.Usage = ResourceUsage.Default;
            indexDesc.SizeInBytes = Utilities.SizeOf<UInt32>() * IndexCount;
            indexDesc.BindFlags = BindFlags.IndexBuffer;
            indexDesc.CpuAccessFlags = CpuAccessFlags.None;
            indexDesc.OptionFlags = ResourceOptionFlags.None;
            indexDesc.StructureByteStride = 0;

            IndexBuffer = Buffer.Create(FearEngineApp.Device, indices, indexDesc);
        }

        public void Render()
        {
            FearEngineApp.Context.InputAssembler.PrimitiveTopology = PrimitiveTopology.LineList;
            FearEngineApp.Context.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(VertexBuffer, VertexLayouts.PositionColor.GetByteSize(), 0));
            FearEngineApp.Context.InputAssembler.SetIndexBuffer(IndexBuffer, Format.R32_UInt, 0);
            FearEngineApp.Context.DrawIndexed(IndexCount, 0, 0);
        }

        public void Dispose()
        {
            VertexBuffer.Dispose();
            IndexBuffer.Dispose();
        }
    }
}
