using FearEngine.Resources.Managment;
using FearEngine.Resources.Managment.Loaders;

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
                    throw new FearEngine.Resources.Managment.ResourceFileFactory.UnknownResourceType();
            }
        }
    }
}
