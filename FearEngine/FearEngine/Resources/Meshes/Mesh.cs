using FearEngine.Resources.Managment;
using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources.Meshes
{
    public class Mesh : Resource
    {
        private Buffer vertexBuffer;
        private Buffer indexBuffer;

        //TODO This should probably be generalised outside of the mesh.
        VertexInputLayout inputLayout;

        private bool isLoaded = false;

        public Mesh(GraphicsDevice graphicsDevice, MeshInformation meshInfo)
        {
            vertexBuffer = Buffer.Vertex.New(graphicsDevice, meshInfo.GetVertices());
            indexBuffer = Buffer.Index.New(graphicsDevice, meshInfo.GetIndices());

            inputLayout = VertexInputLayout.New(0, typeof(VertexLayouts.PositionNormalTexture));
            isLoaded = true;
        }

        public Buffer GetVertexBuffer()
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

        public void Dispose()
        {
            isLoaded = false;
            vertexBuffer.Dispose();
            indexBuffer.Dispose();
        }

        public bool IsLoaded()
        {
            return isLoaded;
        }

        internal int GetVertexStride()
        {
            return System.Runtime.InteropServices.Marshal.SizeOf(typeof(VertexLayouts.PositionNormalTexture));
        }
    }
}
