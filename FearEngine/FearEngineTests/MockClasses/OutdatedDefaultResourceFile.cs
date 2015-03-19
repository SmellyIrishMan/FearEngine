using FearEngine.Resources.Managment;
using FearEngine.Resources.Managment.Loaders;

namespace FearEngineTests
{
    public class OutdatedDefaultResourceFile : ResourceFile
    {
        public OutdatedDefaultResourceFile(string location, ResourceInformation defaultInfo)
            : base(location, defaultInfo)
        {

        }

        override protected ResourceInformation CreateFreshResourceInformation()
        {
            return new MaterialResourceInformation();
        }

        override protected string GetType()
        {
            return "Material";
        }

        override public string GetFilename()
        {
            return "ResourceFileWithOutdatedDefault.xml";
        }
    }
}

