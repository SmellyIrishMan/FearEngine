using FearEngine.Resources.Managment.Loaders;
namespace FearEngine.Resources.Managment
{
    public class MaterialResourceFile : ResourceFile
    {
        public MaterialResourceFile(string location, ResourceInformation defaultInfo)
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
            return "Materials.xml";
        }
    }
}
