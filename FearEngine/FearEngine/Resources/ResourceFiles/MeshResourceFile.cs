namespace FearEngine.Resources.Managment
{
    public class MeshResourceFile : ResourceFile
    {
        public MeshResourceFile(string location) : base(location)
        {

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
