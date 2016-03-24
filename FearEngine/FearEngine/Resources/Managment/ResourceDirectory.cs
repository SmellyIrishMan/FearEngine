using FearEngine.Logger;
using FearEngine.Resources.Loaders;
using FearEngine.Resources.ResourceFiles.ResourceFileInformation;
using System;
using System.Collections.Generic;

namespace FearEngine.Resources.Management
{
    public class ResourceDirectory
    {
        private string directoryName = "FearResources";
        private string resourcesPath;

        Dictionary<ResourceType, ResourceFile> resourceFiles;

        public ResourceDirectory(string rootResourcePath, ResourceFileFactory fileFactory)
        {
            if (!System.IO.Directory.Exists(rootResourcePath))
            {
                FearLog.Log("No Resource Directory Found, Creating One", LogPriority.EXCEPTION);
                System.IO.Directory.CreateDirectory(rootResourcePath);
            }

            resourcesPath = System.IO.Path.Combine(rootResourcePath, directoryName);
            if (!System.IO.Directory.Exists(resourcesPath))
            {
                System.IO.Directory.CreateDirectory(resourcesPath);
            }

            var values = Enum.GetValues(typeof(ResourceType));

            resourceFiles = new Dictionary<ResourceType, ResourceFile>();
            foreach (ResourceType type in values)
            {
                resourceFiles[type] = fileFactory.createResourceFile(type, resourcesPath);
            }

            FearLog.Log("Current Resource Directory; " + rootResourcePath);
        }

        public ResourceInformation GetMaterialInformation(string name)
        {
            return resourceFiles[ResourceType.Material].GetResourceInformationByName(name);
        }

        public ResourceInformation GetMeshInformation(string name)
        {
            return resourceFiles[ResourceType.Mesh].GetResourceInformationByName(name);
        }

        public ResourceInformation GetTextureInformation(string name)
        {
            return resourceFiles[ResourceType.Texture].GetResourceInformationByName(name);
        }

    }
}
