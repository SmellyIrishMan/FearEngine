using FearEngine.Resources.Management;

namespace FearEngine.Resources.ResourceFiles.ResourceFileInformation
{
    public class MaterialResourceInformation : ResourceInformation
    {

        public MaterialResourceInformation() 
            : base ()
        {
        }

        override public ResourceType Type { get { return ResourceType.Material; } }

        override protected void PopulateDefaultValues()
        {
            AddInformation("Technique", "DefaultTechnique");
        }
    }
}
