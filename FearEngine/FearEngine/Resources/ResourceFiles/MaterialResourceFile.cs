namespace FearEngine.Resources.Managment
{
    public class MaterialResourceFile : ResourceFile
    {
        public MaterialResourceFile(string location)
            : base(location)
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
