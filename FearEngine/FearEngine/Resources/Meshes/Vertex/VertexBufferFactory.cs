using FearEngine.Resources.Meshes.Vertex.Layouts;
using SharpDX.Toolkit.Graphics;
using System;

namespace FearEngine.Resources.Meshes
{
    public class VertexBufferFactory
    {
        PNTVertexBuffer pntVertBuff;
        PNTTVertexBuffer pnttVertBuff;

        public VertexBufferFactory()
        {
            pntVertBuff = new PNTVertexBuffer();
            pnttVertBuff = new PNTTVertexBuffer();
        }

        public SharpDX.Toolkit.Graphics.Buffer CreateVertexBuffer(GraphicsDevice graphicsDevice, MeshData meshData, VertexLayout layout)
        {
            if (layout.GetType() == typeof(PositionNormalTextureLayout))
            {
                return pntVertBuff.CreateBufferFromMeshData(graphicsDevice, meshData);
            }

            if (layout.GetType() == typeof(PositionNormalTangentTextureLayout))
            {
                return pnttVertBuff.CreateBufferFromMeshData(graphicsDevice, meshData);
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
