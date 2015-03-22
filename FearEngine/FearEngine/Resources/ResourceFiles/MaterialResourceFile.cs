using FearEngine.Resources.Managment.Loaders;
using FearEngine.Resources.ResourceFiles;

namespace FearEngine.Resources.Managment
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
