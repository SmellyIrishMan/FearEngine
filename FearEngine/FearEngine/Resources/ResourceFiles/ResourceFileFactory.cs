using FearEngine.Resources.Management;
using FearEngine.Resources.ResourceFiles;
using System;

namespace FearEngine.Resources.Loaders
{
    public class ResourceFileFactory
    {

        public ResourceFile createResourceFile(ResourceType type, string path)
        {
            switch(type)
            {
                case ResourceType.Mesh:
                    return new MeshResourceFile(new XMLResourceStorage(path, "Meshes.xml", type));

                case ResourceType.Material:
                    return new MaterialResourceFile(new XMLResourceStorage(path, "Materials.xml", type));

                case ResourceType.Texture:
                    return new TextureResourceFile(new XMLResourceStorage(path, "Textures.xml", type));

                default:
                    throw new UnknownResourceType();
            }
        }

        public class UnknownResourceType : Exception
        {
            public UnknownResourceType()
            {
            }

            public UnknownResourceType(string message)
                : base(message)
            {
            }

            public UnknownResourceType(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
    }
}
