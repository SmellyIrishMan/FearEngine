using FearEngine.Resources.Management;
using FearEngine.Resources.ResourceFiles;
using FearEngine.Resources.ResourceFiles.ResourceFileInformation;

namespace FearEngine.Resources.Loaders
{
    public abstract class ResourceFile
    {
        ResourceStorage storage;

        abstract protected ResourceType Type { get; } 

        public ResourceFile(ResourceStorage store)
        {
            storage = store;
        }

        public ResourceInformation GetResourceInformationByName(string name)
        {
            return storage.GetInformationByName(name);
        }
    }
}
