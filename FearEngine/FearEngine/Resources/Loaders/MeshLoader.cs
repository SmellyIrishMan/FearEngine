using FearEngine.Logger;
using FearEngine.Resources.Managment;
using FearEngine.Resources.Managment.Loaders;
using FearEngine.Resources.Managment.Loaders.Collada;
using FearEngine.Resources.Meshes;
using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources.Loaders
{
    public class MeshLoader : ResourceLoader
    {
        GraphicsDevice device;
        ColladaMeshLoader loader;
        VertexBufferFactory vertBuffFactory;

        public MeshLoader(GraphicsDevice dev, ColladaMeshLoader l, VertexBufferFactory fac)
        {
            device = dev;
            loader = l;
            vertBuffFactory = fac;
        }

        public Resource Load(ResourceInformation info)
        {
            Mesh mesh = new Mesh(device, loader.Load(info.Filepath), vertBuffFactory);

            return mesh;
        }
    }
}
