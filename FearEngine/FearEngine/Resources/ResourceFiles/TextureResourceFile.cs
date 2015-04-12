using FearEngine.Resources.Management;
using FearEngine.Resources.ResourceFiles;

namespace FearEngine.Resources.Loaders
{
    public class TextureResourceFile : ResourceFile
    {
        public TextureResourceFile(ResourceStorage store)
            : base(store)
        {

        }

        override protected ResourceType Type { get { return ResourceType.Texture; } }
    }
}
