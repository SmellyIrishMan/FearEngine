﻿using FearEngine.Logger;
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

        public MeshLoader(GraphicsDevice dev)
        {
            device = dev;
        }

        public Resource Load(ResourceInformation info)
        {
            ColladaMeshLoader meshLoader = new ColladaMeshLoader();

            RenderableMesh mesh = new RenderableMesh(device, meshLoader.Load(info.Filepath), new PositionNormalTextureLayout());

            return mesh;
        }
    }
}
