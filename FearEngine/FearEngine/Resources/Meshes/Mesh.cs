using FearEngine.Resources.Managment;
using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources.Meshes
{
    public class Mesh : Resource
    {
        private Buffer<VertexLayouts.PositionNormalTexture> vertexBuffer;
        private Buffer indexBuffer;

        //TODO This should probably be generalised outside of the mesh.
        VertexInputLayout inputLayout;

        private bool isLoaded = false;

        public Mesh(GraphicsDevice graphicsDevice, MeshInformation meshInfo)
        {
            vertexBuffer = Buffer.Vertex.New(graphicsDevice, meshInfo.GetVertices());
            indexBuffer = Buffer.Index.New(graphicsDevice, meshInfo.GetIndices());

            inputLayout = VertexInputLayout.FromBuffer(0, vertexBuffer);
            isLoaded = true;
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
    }
}
