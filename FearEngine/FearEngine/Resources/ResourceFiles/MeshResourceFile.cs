using FearEngine.Resources.Managment.Loaders;
using FearEngine.Resources.ResourceFiles;

namespace FearEngine.Resources.Managment
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
