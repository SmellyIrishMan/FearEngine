using FearEngine.Resources.Managment.Loaders;
namespace FearEngine.Resources.Managment
{
    public class TextureResourceFile : ResourceFile
    {
        public TextureResourceFile(string location, ResourceInformation defaultInfo)
            : base(location, defaultInfo)
        {

        }

        override protected ResourceInformation CreateFreshResourceInformation()
        {
            return new TextureResourceInformation();
        }

        override protected string GetType()
        {
            return "Texture";
        }

        override public string GetFilename()
        {
            return "Textures.xml";
        }
    }
}
