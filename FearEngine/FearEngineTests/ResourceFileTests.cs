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
        public void GetMeshFromFile()
        {
            //Given

            //When

            //Then
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void GetMeshFromFileThatDoesNotExist()
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
        public void GetMeshFromFileThatDoesNotExistFallbackToDefault()
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
