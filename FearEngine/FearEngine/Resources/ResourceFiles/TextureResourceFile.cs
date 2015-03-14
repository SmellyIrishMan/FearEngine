namespace FearEngine.Resources.Managment
{
    public class TextureResourceFile : ResourceFile
    {
        public TextureResourceFile(string location)
            : base(location)
        {

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
