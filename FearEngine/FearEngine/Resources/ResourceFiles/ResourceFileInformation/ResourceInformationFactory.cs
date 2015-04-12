using FearEngine.Resources.Loaders.Loaders;
using FearEngine.Resources.Management;

namespace FearEngine.Resources.ResourceFiles.ResourceFileInformation
{
    class ResourceInformationFactory
    {
        public ResourceInformation CreateResourceInformation(ResourceType type)
        {
            switch (type)
            {
                case ResourceType.Mesh:
                    return new MeshResourceInformation();

                case ResourceType.Material:
                    return new MaterialResourceInformation();

                case ResourceType.Texture:
                    return new TextureResourceInformation();

                default:
                    throw new FearEngine.Resources.Loaders.ResourceFileFactory.UnknownResourceType();
            }
        }
    }
}
