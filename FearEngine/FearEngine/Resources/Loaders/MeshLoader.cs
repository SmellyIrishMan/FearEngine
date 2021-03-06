﻿using FearEngine.Resources.Loaders.Loaders.Collada;
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

        public MeshLoader(FearGraphicsDevice dev, ColladaMeshLoader formatLoader, VertexBufferFactory vertBufferFactory)
        {
            device = dev.Device;
            loader = formatLoader;
            vertBuffFactory = vertBufferFactory;
        }

        public Resource Load(ResourceInformation info)
        {
            Mesh mesh = new Mesh(device, loader.Load(info.Filepath), vertBuffFactory);

            return mesh;
        }
    }
}
