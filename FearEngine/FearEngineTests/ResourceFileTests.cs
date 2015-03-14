using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine.Resources.Managment;
using System.IO;

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
            MeshResourceFile testFile = new MeshResourceFile(GetResourceFolder());
            
            //When
            string filePath = testFile.GetResouceInformationByName("TEAPOT").GetFilepath();

            //Then
            string originalFilePath = "C:\\Users\\Andy\\Documents\\Coding\\Visual Studio 2012\\Projects\\FearEngine\\Resources\\Models\\Teapot.DAE";
            Assert.IsTrue(filePath.CompareTo(originalFilePath) == 0);
        }

        [TestMethod]
        public void AddDefaultMeshToFileWithoutDefaultMesh()
        {
            //Given
            NoDefaultResourceFile testFile = new NoDefaultResourceFile(GetResourceFolder());

            //When
            string filePath = testFile.GetResouceInformationByName("DEFAULT").GetFilepath();

            //Then
            Assert.IsTrue(filePath.CompareTo("") == 0);
        }

        [TestMethod]
        public void GetMeshResourceThatDoesNotExist()
        {
            //Given
            MeshResourceFile testFile = new MeshResourceFile(GetResourceFolder());

            //When
            string filePath = testFile.GetResouceInformationByName("THISMESHDOESNOTEXIST").GetFilepath();

            //Then
            Assert.IsTrue(filePath.CompareTo("") == 0);
        }

        [TestMethod]
        public void GetMeshResourceThatDoesNotExistAndFallbackToDefault()
        {
            //Given
            MeshResourceFile testFile = new MeshResourceFile(GetResourceFolder());

            //When
            string filePath = testFile.GetResouceInformationByName("THISMESHDOESNOTEXIST", true).GetFilepath();

            //Then
            string originalFilePath = "C:\\Users\\Andy\\Documents\\Coding\\Visual Studio 2012\\Projects\\FearEngine\\Resources\\Models\\Box.DAE";
            Assert.IsTrue(filePath.CompareTo(originalFilePath) == 0);
        }
    }
}
