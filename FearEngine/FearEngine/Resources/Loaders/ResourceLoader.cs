using FearEngine.Resources.Management;
using FearEngine.Resources.ResourceFiles.ResourceFileInformation;

namespace FearEngine.Resources.Loaders
{
    public interface ResourceLoader
    {
        Resource Load(ResourceInformation info);
    }
}
