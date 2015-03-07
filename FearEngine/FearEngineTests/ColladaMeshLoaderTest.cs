using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine.Resources.Meshes;
using FearEngine.Resources.Managment.Loaders.Collada;

namespace FearEngineTests
{
    [TestClass]
    public class ColladaMeshLoaderTest
    {
        [TestMethod]
        public void LoadMeshFromFileThatDoesntExist()
        {
            //Given
            ColladaMeshLoader loader = new ColladaMeshLoader();
            
            //When
            MeshInformation mesh = loader.Load("ThisFileDoesNotExist.DAE");

            //Then
            MeshInformation newMesh = new MeshInformation();
            Assert.AreEqual(mesh, newMesh);
        }

        [TestMethod]
        public void LoadMeshFromFile()
        {
            //Given
            ColladaMeshLoader loader = new ColladaMeshLoader();

            //When
            MeshInformation mesh = loader.Load("Resources/Box.DAE");
            
            //Then
            Assert.IsTrue(mesh.GetIndexCount() == (uint)36);
            Assert.IsTrue(mesh.GetVertexCount() == (uint)36);
        }
    }
}
