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
                    const string defaultMeshFile = "C:\\Users\\Andy\\Documents\\Coding\\Visual Studio 2012\\Projects\\FearEngine\\Resources\\Models\\Box.DAE";
                    return new MeshResourceFile(path, defaultMeshFile);

                case ResourceType.Material:
                    const string defaultMaterialFile = "C:\\Users\\Andy\\Documents\\Coding\\Visual Studio 2012\\Projects\\FearEngine\\Resources\\Shaders\\Basic.fx";
                    return new MaterialResourceFile(path, defaultMaterialFile);

                case ResourceType.Texture:
                    const string defaultTextureFile = "";
                    return new TextureResourceFile(path, defaultTextureFile);

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
