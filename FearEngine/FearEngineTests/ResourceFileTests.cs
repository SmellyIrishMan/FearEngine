using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine.Resources.Managment;
using System.IO;

namespace FearEngineTests
{
    [TestClass]
    public class ResourceFileTests
    {
        [TestMethod]
        public void AddDefaultMeshToFileWithoutDefaultMesh()
        {
            //Given
            DirectoryInfo resourceDir = new System.IO.DirectoryInfo(System.Environment.CurrentDirectory);
            string resourcesPath = System.IO.Path.Combine(resourceDir.FullName, "Resources");
            string defaultMeshPath = resourcesPath + "\\Box.DAE";

            //When
            NoDefaultResourceFile testFile = new NoDefaultResourceFile(resourcesPath, defaultMeshPath);
            string filePath = testFile.GetFilepathByResourceName("DEFAULT");

            //Then
            Assert.IsTrue(filePath.CompareTo(defaultMeshPath) == 0);
        }

        [TestMethod]
        public void CreateFileWithoutOverwritingExistingFile()
        {
            //Given
            DirectoryInfo resourceDir = new System.IO.DirectoryInfo(System.Environment.CurrentDirectory);
            string resourcesPath = System.IO.Path.Combine(resourceDir.FullName, "Resources");
            string defaultMeshPath = resourcesPath + "\\Box.DAE";

            //When
            MeshResourceFile testFile = new MeshResourceFile(resourcesPath, defaultMeshPath);
            string filePath = testFile.GetFilepathByResourceName("TEAPOT");

            //Then
            Assert.IsTrue(filePath.CompareTo("") != 0);
        }

        [TestMethod]
        public void GetMeshThatDoesNotExistFromFile()
        {
            //Given
            string currentDir = System.Environment.CurrentDirectory;
            string defaultFilePath = currentDir + "\\Test.Dae";
            MeshResourceFile testFile = new MeshResourceFile(currentDir, defaultFilePath);

            //When
            string filePath = testFile.GetFilepathByResourceName("THISMESHDOESNOTEXIST");

            //Then
            Assert.IsTrue(filePath.CompareTo("") == 0);
        }

        [TestMethod]
        public void GetMeshThatDoesNotExistFromFileAndFallbackToDefault()
        {
            //Given
            string currentDir = System.Environment.CurrentDirectory;
            string defaultFilePath = currentDir + "\\Test.Dae";
            MeshResourceFile testFile = new MeshResourceFile(currentDir, defaultFilePath);

            //When
            string filePath = testFile.GetFilepathByResourceName("THISMESHDOESNOTEXIST", true);

            //Then
            int check = filePath.CompareTo(defaultFilePath);
            Assert.IsTrue(check == 0);
        }
    }
}
