namespace FearEngine.Resources.Managment
{
    public class MeshResourceFile : ResourceFile
    {
        public MeshResourceFile(string location, string defautFilePath) : base(location, defautFilePath)
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
