using FearEngine.Logger;
using System;
using System.Collections.Generic;

namespace FearEngine.Resources.Managment.Loaders
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
