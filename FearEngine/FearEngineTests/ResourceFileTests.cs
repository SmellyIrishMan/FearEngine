using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine.Resources.Managment;
using System.IO;
using FearEngine.Resources.Managment.Loaders;
using FearEngine.Resources.ResourceFiles;

namespace FearEngineTests
{
    [TestClass]
    public class ResourceFileTests
    {
        private string GetResourceFolder()
        {
            DirectoryInfo resourceDir = new System.IO.DirectoryInfo(System.Environment.CurrentDirectory);
            string resourcesPath = System.IO.Path.Combine(resourceDir.FullName, "Resources");

            return resourcesPath;
        }
        
        [TestMethod]
        public void CreateResourceFileWithoutOverwritingExistingFile()
        {
            //Given
            MeshResourceFile testFile = new MeshResourceFile(new XMLResourceStorage(GetResourceFolder(), "Meshes.xml", ResourceType.Mesh));
            
            //When
            string filePath = testFile.GetResourceInformationByName("TEAPOT").Filepath;

            //Then
            string originalFilePath = "C:\\Users\\Andy\\Documents\\Coding\\Visual Studio 2012\\Projects\\FearEngine\\Resources\\Models\\Teapot.DAE";
            Assert.IsTrue(filePath.CompareTo(originalFilePath) == 0);
        }

        [TestMethod]
        public void AddDefaultMeshToFileWithoutDefaultMesh()
        {
            //Given
            ResourceFile noDefaultResourceFile = new MeshResourceFile(new XMLResourceStorage(GetResourceFolder(), "ResourceFileWithoutDefault.xml", ResourceType.Mesh));

            //When
            string filePath = noDefaultResourceFile.GetResourceInformationByName("DEFAULT").Filepath;

            //Then
            Assert.IsTrue(filePath.CompareTo("") == 0);
        }

        [TestMethod]
        public void GetMeshResourceThatDoesNotExistAndFallbackToDefault()
        {
            //Given
            MeshResourceFile testFile = new MeshResourceFile(new XMLResourceStorage(GetResourceFolder(), "Meshes.xml", ResourceType.Mesh));

            //When
            string filePath = testFile.GetResourceInformationByName("THISMESHDOESNOTEXIST").Filepath;

            //Then
            string originalFilePath = "C:\\Users\\Andy\\Documents\\Coding\\Visual Studio 2012\\Projects\\FearEngine\\Resources\\Models\\Box.DAE";
            Assert.IsTrue(filePath.CompareTo(originalFilePath) == 0);
        }

        [TestMethod]
        public void UpdateDefaultInExistingResourceFile()
        {
            //Given
            ResourceFile outOfDateDefaultResourceFile = new MeshResourceFile(new XMLResourceStorage(GetResourceFolder(), "ResourceFileWithOutdatedDefault.xml", ResourceType.Material));
            MaterialResourceInformation defaultInfo = new MaterialResourceInformation();

            //When
            ResourceInformation updatedInformation = outOfDateDefaultResourceFile.GetResourceInformationByName("DEFAULT");
            string updatedFilePath = updatedInformation.Filepath;

            //Then
            string originalFilePath = "C:\\ThisAddressShouldStayTheSame";
            Assert.IsTrue(updatedFilePath.CompareTo(originalFilePath) == 0);
            Assert.IsTrue(updatedInformation.InformationKeys.Count == defaultInfo.InformationKeys.Count);
        }
    }
}
