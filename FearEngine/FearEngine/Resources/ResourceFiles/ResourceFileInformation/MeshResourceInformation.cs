using FearEngine.Resources.Management;

namespace FearEngine.Resources.ResourceFiles.ResourceFileInformation
{
    public class MeshResourceInformation : ResourceInformation
    {
        public MeshResourceInformation()
            : base()
        {
        }

        override public ResourceType Type { get { return ResourceType.Mesh; } }

        override protected void PopulateDefaultValues()
        {
           
        }
    }
}
