using FearEngine.Resources.Managment;

namespace FearEngineTests
{
    public class NoDefaultResourceFile : ResourceFile
    {
        public NoDefaultResourceFile(string location, string defautFilePath)
            : base(location, defautFilePath)
        {

        }

        override protected string GetType()
        {
            return "Mesh";
        }

        override public string GetFilename()
        {
            return "ResourceFileWithoutDefault.xml";
        }
    }
}
