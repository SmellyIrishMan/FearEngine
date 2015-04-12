using FearEngine.Resources.Management;
using FearEngine.Resources.ResourceFiles;

namespace FearEngine.Resources.Loaders
{
    public class MeshResourceFile : ResourceFile
    {
        public MeshResourceFile(ResourceStorage store)
            : base(store)
        {

        }

        override protected ResourceType Type { get { return ResourceType.Mesh; } }
    }
}
