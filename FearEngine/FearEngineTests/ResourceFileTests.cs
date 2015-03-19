using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine.Resources.Managment;
using System.IO;
using FearEngine.Resources.Managment.Loaders;

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
        public void CreateFileWithoutOverwritingExistingFile()
        {
            //Given
            MeshResourceFile testFile = new MeshResourceFile(GetResourceFolder(), new MeshResourceInformation());
            
            //When
            string filePath = testFile.GetResourceInformationByName("TEAPOT").GetFilepath();

            //Then
            string originalFilePath = "C:\\Users\\Andy\\Documents\\Coding\\Visual Studio 2012\\Projects\\FearEngine\\Resources\\Models\\Teapot.DAE";
            Assert.IsTrue(filePath.CompareTo(originalFilePath) == 0);
        }

        [TestMethod]
        public void AddDefaultMeshToFileWithoutDefaultMesh()
        {
            //Given
            NoDefaultResourceFile testFile = new NoDefaultResourceFile(GetResourceFolder(), new MeshResourceInformation());

            //When
            string filePath = testFile.GetResourceInformationByName("DEFAULT").GetFilepath();

            //Then
            Assert.IsTrue(filePath.CompareTo("") == 0);
        }

        [TestMethod]
        public void GetMeshResourceThatDoesNotExist()
        {
            //Given
            MeshResourceFile testFile = new MeshResourceFile(GetResourceFolder(), new MeshResourceInformation());

            //When
            string filePath = testFile.GetResourceInformationByName("THISMESHDOESNOTEXIST").GetFilepath();

            //Then
            Assert.IsTrue(filePath.CompareTo("") == 0);
        }

        [TestMethod]
        public void GetMeshResourceThatDoesNotExistAndFallbackToDefault()
        {
            //Given
            MeshResourceFile testFile = new MeshResourceFile(GetResourceFolder(), new MeshResourceInformation());

            //When
            string filePath = testFile.GetResourceInformationByName("THISMESHDOESNOTEXIST", true).GetFilepath();

            //Then
            string originalFilePath = "C:\\Users\\Andy\\Documents\\Coding\\Visual Studio 2012\\Projects\\FearEngine\\Resources\\Models\\Box.DAE";
            Assert.IsTrue(filePath.CompareTo(originalFilePath) == 0);
        }

        [TestMethod]
        public void UpdateDefaultInExistingResourceFile()
        {
            //Given
            OutdatedDefaultResourceFile testFile = new OutdatedDefaultResourceFile(GetResourceFolder(), new MaterialResourceInformation());
            MaterialResourceInformation defaultInfo = new MaterialResourceInformation();

            //When
            ResourceInformation updatedInformation = testFile.GetResourceInformationByName("DEFAULT");
            string updatedFilePath = updatedInformation.GetFilepath();

            //Then
            string originalFilePath = "C:\\ThisAddressShouldStayTheSame";
            Assert.IsTrue(updatedFilePath.CompareTo(originalFilePath) == 0);
            Assert.IsTrue(updatedInformation.GetInformationKeys().Count == defaultInfo.GetInformationKeys().Count);
        }
    }
}
