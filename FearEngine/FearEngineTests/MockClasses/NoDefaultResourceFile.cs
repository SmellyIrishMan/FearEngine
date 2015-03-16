using FearEngine.Resources.Managment;
using FearEngine.Resources.Managment.Loaders;

namespace FearEngineTests
{
    public class NoDefaultResourceFile : ResourceFile
    {
        public NoDefaultResourceFile(string location, ResourceInformation defaultInfo)
            : base(location, defaultInfo)
        {

        }

        override protected ResourceInformation CreateFreshResourceInformation()
        {
            return new MeshResourceInformation();
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
