using FearEngine.Resources.Management;
using FearEngine.Resources.ResourceFiles;

namespace FearEngine.Resources.Loaders
{
    public class MaterialResourceFile : ResourceFile
    {
        public MaterialResourceFile(ResourceStorage store)
            : base(store)
        {

        }

        override protected ResourceType Type { get { return ResourceType.Material; } }
    }
}
