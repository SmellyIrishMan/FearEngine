using FearEngine.Resources.ResourceFiles.ResourceFileInformation;

namespace FearEngine.Resources.ResourceFiles
{
    public interface ResourceStorage
    {
        ResourceInformation GetInformationByName(string name);

        void StoreInformation(ResourceInformation information);
    }
}
