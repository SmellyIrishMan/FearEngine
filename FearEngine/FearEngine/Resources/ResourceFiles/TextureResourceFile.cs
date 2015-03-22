using FearEngine.Resources.Managment.Loaders;
using FearEngine.Resources.ResourceFiles;

namespace FearEngine.Resources.Managment
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
