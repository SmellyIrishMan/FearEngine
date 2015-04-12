using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine.Resources.Loaders;
using System.IO;
using FearEngine.Resources.Management;

namespace FearEngineTests
{
    [TestClass]
    public class ResourceDirectoryTests
    {
        [TestMethod]
        public void CreateABlankResourceDirectoryWithAllResourceFiles()
        {
            //Given
            DirectoryInfo resourceDir = new System.IO.DirectoryInfo(System.Environment.CurrentDirectory);
            string resourcesPath = System.IO.Path.Combine(resourceDir.FullName, "FreshResourcesSetup");

            if(System.IO.Directory.Exists(resourcesPath))
            {
                System.IO.Directory.Delete(resourcesPath, true);
            }

            ResourceFileFactory fileFactory = new ResourceFileFactory();

            //When
            ResourceDirectory resDir = new ResourceDirectory(resourcesPath, fileFactory);

            //Then
            Assert.IsTrue(System.IO.Directory.Exists(resourcesPath));

            Assert.IsTrue(System.IO.File.Exists(resourcesPath + "\\FearResources\\Meshes.xml"));
            Assert.IsTrue(System.IO.File.Exists(resourcesPath + "\\FearResources\\Materials.xml"));
            Assert.IsTrue(System.IO.File.Exists(resourcesPath + "\\FearResources\\Textures.xml"));
        }
    }
}
