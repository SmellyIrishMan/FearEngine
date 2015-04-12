using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine.Resources.Meshes;
using FearEngine.Resources.Loaders.Loaders.Collada;

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
            MeshData mesh = loader.Load("ThisFileDoesNotExist.DAE");

            //Then
            MeshData newMesh = new MeshData();
            Assert.AreEqual(mesh, newMesh);
        }

        [TestMethod]
        public void LoadMeshFromFile()
        {
            //Given
            ColladaMeshLoader loader = new ColladaMeshLoader();

            //When
            MeshData mesh = loader.Load("Resources/Box.DAE");
            
            //Then
            Assert.IsTrue(mesh.GetIndexCount() == (uint)36);
            Assert.IsTrue(mesh.GetVertexCount() == (uint)36);
        }
    }
}
