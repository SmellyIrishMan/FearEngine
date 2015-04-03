using FearEngine.GameObjects;
using FearEngine.Resources.Managment;
using FearEngine.Resources.Meshes.Vertex.Layouts;
using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources.Meshes
{
    public class Mesh : Resource
    {
        private GraphicsDevice device;

        private Buffer vertexBuffer;
        private Buffer indexBuffer;
        private VertexLayout vertLayout;

        private bool isLoaded = false;

        public Mesh(GraphicsDevice graphicsDevice, MeshData meshData, VertexBufferFactory vertBuffFactory)
        {
            device = graphicsDevice;

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

        public void Render()
        {
            device.SetVertexBuffer(0, GetVertexBuffer(), GetVertexStride());
            device.SetVertexInputLayout(GetInputLayout());

            device.SetIndexBuffer(GetIndexBuffer(), true);

            device.DrawIndexed(PrimitiveType.TriangleList, GetIndexBuffer().ElementCount);
        }
    }
}
