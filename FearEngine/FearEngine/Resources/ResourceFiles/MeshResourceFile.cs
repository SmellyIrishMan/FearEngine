using FearEngine.Resources.Managment.Loaders;
namespace FearEngine.Resources.Managment
{
    public class MeshResourceFile : ResourceFile
    {
        public MeshResourceFile(string location, ResourceInformation defaultInfo)
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
            return "Meshes.xml";
        }
    }
}
