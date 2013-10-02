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
    public abstract class Plane
    {
        protected Point Size { get; set; }  //How many points in the plane? 

        int VertexCount { get; set; }
        int IndexCount { get; set; }

        protected VertexLayouts.PositionNormalTexture[] Vertices { get; set; }
        protected UInt32[] Indices { get; set; }

        Buffer VertexBuffer { get; set; }
        Buffer IndexBuffer { get; set; }

        public Plane(Point size)
        {
            Size = size;
            VertexCount = Size.X * Size.Y;
            IndexCount = (Size.X - 1) * (Size.Y - 1 ) * 6;

            VertexBuffer = IndexBuffer = null;

            Vertices = new VertexLayouts.PositionNormalTexture[VertexCount];
            Indices = new UInt32[IndexCount];
        }

        public void Initialise()
        {
            InitialiseVerticesAndIndicies();
            CreateBuffers();
        }

        protected virtual void InitialiseVerticesAndIndicies()
        {
            UInt32 index = 0;

            for (int j = 0; j < Size.Y; j++)
            {
                for (int i = 0; i < Size.X; i++)
                {
                    Vertices[index].Position = new Vector3(i, 0.0f, j);
                    Vertices[index].Normal = new Vector3(0.0f, 1.0f, 0.0f);
                    Vertices[index].TexCoord = new Vector2((float)i / (float)(Size.X - 1), (float)j / (float)(Size.Y - 1));
                    index++;
                }
            }

            InitialiseIndices();
        }

        protected void InitialiseIndices()
        {
            UInt32 index = 0;
            for (int j = 0; j < Size.Y - 1; j++)
            {
                for (int i = 0; i < Size.X - 1; i++)
                {
                    //Bottom Left
                    Indices[index] = (uint)((j * Size.X) + i);
                    index++;
                    //Top Left
                    Indices[index] = (uint)(((j + 1) * Size.X) + i);
                    index++;
                    //Top Right
                    Indices[index] = (uint)(((j + 1) * Size.X) + (i + 1));
                    index++;
                    //Bottom Left
                    Indices[index] = (uint)((j * Size.X) + i);
                    index++;
                    //Top Right
                    Indices[index] = (uint)(((j + 1) * Size.X) + (i + 1));
                    index++;
                    //Bottom Right
                    Indices[index] = (uint)((j * Size.X) + (i + 1));
                    index++;
                }
            }
        }

        private void CreateBuffers()
        {
            BufferDescription vertDesc = new BufferDescription();
            vertDesc.Usage = ResourceUsage.Default;
            vertDesc.SizeInBytes = VertexLayouts.PositionNormalTexture.GetByteSize() * VertexCount;
            vertDesc.BindFlags = BindFlags.VertexBuffer;
            vertDesc.CpuAccessFlags = CpuAccessFlags.None;
            vertDesc.OptionFlags = ResourceOptionFlags.None;
            vertDesc.StructureByteStride = 0;
            VertexBuffer = Buffer.Create(FearEngineApp.Device, Vertices, vertDesc);

            BufferDescription indexDesc = new BufferDescription();
            indexDesc.Usage = ResourceUsage.Default;
            indexDesc.SizeInBytes = Utilities.SizeOf<UInt32>() * IndexCount;
            indexDesc.BindFlags = BindFlags.IndexBuffer;
            indexDesc.CpuAccessFlags = CpuAccessFlags.None;
            indexDesc.OptionFlags = ResourceOptionFlags.None;
            indexDesc.StructureByteStride = 0;
            IndexBuffer = Buffer.Create(FearEngineApp.Device, Indices, indexDesc);
        }

        public void Render()
        {
            FearEngineApp.Context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            FearEngineApp.Context.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(VertexBuffer, VertexLayouts.PositionNormalTexture.GetByteSize(), 0));
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
