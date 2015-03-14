namespace FearEngine.Resources.Managment
{
    public class MaterialResourceFile : ResourceFile
    {
        public MaterialResourceFile(string location, string defautFilePath)
            : base(location, defautFilePath)
        {

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
