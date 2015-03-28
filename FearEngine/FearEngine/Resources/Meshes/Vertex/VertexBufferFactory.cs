using FearEngine.Resources.Meshes.Vertex.Layouts;
using SharpDX.Toolkit.Graphics;
using System;

namespace FearEngine.Resources.Meshes
{
    public class VertexBufferFactory
    {
        PNTVertexBuffer pntVertBuff;

        public VertexBufferFactory()
        {
            pntVertBuff = new PNTVertexBuffer();
        }

        public SharpDX.Toolkit.Graphics.Buffer CreateVertexBuffer(GraphicsDevice graphicsDevice, MeshData meshData, VertexLayout layout)
        {
            if(layout.GetType() == typeof(PositionNormalTextureLayout))
            {
                return pntVertBuff.CreateBufferFromMeshData(graphicsDevice, meshData);
            }

            throw new UnableToCreateVertexBufferException();
        }
    }

    public class UnableToCreateVertexBufferException : Exception
    {
        public UnableToCreateVertexBufferException()
        {
        }

        public UnableToCreateVertexBufferException(string message)
            : base(message)
        {
        }

        public UnableToCreateVertexBufferException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
