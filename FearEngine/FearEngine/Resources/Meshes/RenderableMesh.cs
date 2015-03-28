using FearEngine.Resources.Managment;
using FearEngine.Resources.Meshes.Vertex.Layouts;
using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources.Meshes
{
    public class RenderableMesh : Resource
    {
        private Buffer vertexBuffer;
        private Buffer indexBuffer;
        private VertexLayout vertLayout;

        private bool isLoaded = false;

        public RenderableMesh(GraphicsDevice graphicsDevice, MeshData meshData, VertexBufferFactory vertBuffFactory)
        {
            vertLayout = VertexLayouts.DetermineLayout(meshData);
            vertexBuffer = vertBuffFactory.CreateVertexBuffer(graphicsDevice, meshData, vertLayout);
            
            indexBuffer = Buffer.Index.New(graphicsDevice, meshData.GetIndices());

            isLoaded = true;
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
