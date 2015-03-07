using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources.Meshes
{
    public class MeshRenderable
    {
        private Buffer<VertexLayouts.PositionNormalTexture> vertexBuffer;
        private Buffer indexBuffer;

        //TODO This should probably be generalised outside of the mesh.
        VertexInputLayout inputLayout;

        public MeshRenderable(GraphicsDevice graphicsDevice, MeshInformation meshInfo)
        {
            vertexBuffer = Buffer.Vertex.New(graphicsDevice, meshInfo.GetVertices());
            indexBuffer = Buffer.Index.New(graphicsDevice, meshInfo.GetIndices());

            inputLayout = VertexInputLayout.FromBuffer(0, vertexBuffer);
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
            vertexBuffer.Dispose();
            indexBuffer.Dispose();
        }
    }
}
