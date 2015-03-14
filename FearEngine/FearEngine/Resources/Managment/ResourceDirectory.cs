using FearEngine.Logger;
using FearEngine.Resources.Managment.Loaders;
using FearEngine.Resources.Managment.Loaders.Collada;
using FearEngine.Resources.Meshes;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;

namespace FearEngine.Resources.Managment
{
    public class ResourceDirectory
    {
        private string directoryName = "FearResources";
        private string resourcesPath;

        Dictionary<ResourceType, ResourceFile> resourceFiles;

        public ResourceDirectory(string rootResourcePath, ResourceFileFactory fileFactory)
        {
            if (System.IO.Directory.Exists(rootResourcePath))
            {

            }
            else
            {
                FearLog.Log("No Resource Directory Found, Creating One", LogPriority.EXCEPTION);
                System.IO.Directory.CreateDirectory(rootResourcePath);

                resourcesPath = System.IO.Path.Combine(rootResourcePath, directoryName);
                System.IO.Directory.CreateDirectory(resourcesPath);

                var values = Enum.GetValues(typeof(ResourceType));

                resourceFiles = new Dictionary<ResourceType, ResourceFile>();
                foreach (ResourceType type in values)
                {
                    resourceFiles[type] = fileFactory.createResourceFile(type, resourcesPath);
                }
            }
            FearLog.Log("Current Resource Directory; " + rootResourcePath);
        }

        public bool IsFullyFormed()
        {
            foreach (ResourceFile file in resourceFiles.Values)
            {
                System.IO.File.Exists(resourcesPath + "\\" + file.GetFilename());
            }

            return true;
        }

        public string GetDefaultResourceName(ResourceType type)
        {
            return resourceFiles[type].GetDefaultResourceName();
        }

        public ResourceInformation GetMaterialInformation(string name)
        {
            return resourceFiles[ResourceType.Material].GetResouceInformationByName(name);
        }

        public ResourceInformation GetMeshInformation(string name)
        {
            return resourceFiles[ResourceType.Mesh].GetResouceInformationByName(name);
        }

        public ResourceInformation GetTextureInformation(string name)
        {
            return resourceFiles[ResourceType.Texture].GetResouceInformationByName(name);
        }

    }
}
