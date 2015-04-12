using FearEngine.Resources.Loaders.Loaders.Collada;
using FearEngine.Resources.Management;
using FearEngine.Resources.Meshes;
using FearEngine.Resources.ResourceFiles.ResourceFileInformation;
using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources.Loaders
{
    public class MeshLoader : ResourceLoader
    {
        GraphicsDevice device;
        ColladaMeshLoader loader;
        VertexBufferFactory vertBuffFactory;

        public MeshLoader(FearGraphicsDevice dev, ColladaMeshLoader l, VertexBufferFactory fac)
        {
            device = dev.Device;
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
