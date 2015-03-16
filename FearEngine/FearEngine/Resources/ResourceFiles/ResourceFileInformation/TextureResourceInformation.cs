using FearEngine.Logger;
using System;
using System.Collections.Generic;

namespace FearEngine.Resources.Managment.Loaders
{
    public class TextureResourceInformation : ResourceInformation
    {
        public TextureResourceInformation() 
            : base ()
        {
        }

        override protected void PopulateDefaultValues()
        {
            AddInformation("IsLinear", "true");
        }
    }
}
