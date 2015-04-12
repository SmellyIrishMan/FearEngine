using FearEngine.Resources.Management;

namespace FearEngine.Resources.ResourceFiles.ResourceFileInformation
{
    public class TextureResourceInformation : ResourceInformation
    {
        public TextureResourceInformation() 
            : base ()
        {
        }

        override public ResourceType Type { get { return ResourceType.Texture; } }

        override protected void PopulateDefaultValues()
        {
            AddInformation("IsLinear", "true");
        }
    }
}
