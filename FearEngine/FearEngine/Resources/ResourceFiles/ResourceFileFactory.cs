using FearEngine.Resources.Managment.Loaders;
using System;
namespace FearEngine.Resources.Managment
{
    public class ResourceFileFactory
    {

        public ResourceFile createResourceFile(ResourceType type, string path)
        {
            switch(type)
            {
                case ResourceType.Mesh:
                    return new MeshResourceFile(path, new MeshResourceInformation());

                case ResourceType.Material:
                    return new MaterialResourceFile(path, new MaterialResourceInformation());

                case ResourceType.Texture:
                    return new TextureResourceFile(path, new TextureResourceInformation());

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
