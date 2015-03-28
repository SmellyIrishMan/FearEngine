using FearEngine.Resources.Managment;
using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources.Meshes
{
    public class RenderableMesh : Resource
    {
        private Buffer vertexBuffer;
        private Buffer indexBuffer;
        private PositionNormalTextureLayout vertLayout;

        private bool isLoaded = false;

        public RenderableMesh(GraphicsDevice graphicsDevice, MeshData meshData, PositionNormalTextureLayout layout)
        {
            CreateVertexBuffer(graphicsDevice, meshData, layout);
            indexBuffer = Buffer.Index.New(graphicsDevice, meshData.GetIndices());

            vertLayout = layout;

            isLoaded = true;
        }

        private void CreateVertexBuffer(GraphicsDevice graphicsDevice, MeshData meshData, PositionNormalTextureLayout layout)
        {
            vertexBuffer = Buffer.Vertex.New(graphicsDevice, layout.GetVertices(meshData.GetVertexData(), meshData.GetVertexCount()));
        }

        public Buffer GetVertexBuffer()
        {
            return vertexBuffer;
        }

        internal int GetVertexStride()
        {
            return vertLayout.GetStride();
        }

        public Buffer GetIndexBuffer()
        {
            return indexBuffer;
        }

        public VertexInputLayout GetInputLayout()
        {
            return vertLayout.GetInputLayout();
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
