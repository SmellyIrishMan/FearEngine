using FearEngine.Logger;
using FearEngine.Resources.Managment.Loaders;
using FearEngine.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace FearEngine.Resources.Managment
{
    public abstract class ResourceFile
    {
        ResourceStorage storage;

        abstract protected ResourceType Type { get; } 

        public ResourceFile(ResourceStorage store)
        {
            storage = store;
        }

        public ResourceInformation GetResourceInformationByName(string name)
        {
            return storage.GetInformationByName(name);
        }
    }
}
