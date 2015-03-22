﻿using FearEngine.Logger;
using System;
using System.Collections.Generic;

namespace FearEngine.Resources.Managment.Loaders
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
