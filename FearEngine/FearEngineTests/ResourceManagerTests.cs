using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine.Resources.Managment;

namespace FearEngineTests
{
    [TestClass]
    public class ResourceManagerTests
    {
        [TestMethod]
        public void StartResourceManager()
        {
            //Given
            FearResourceManager resourceManager = new FearResourceManager(null, null, null, null);
          
            //When

            //Then
            Assert.IsTrue(resourceManager.GetNumberOfLoadedResources(ResourceType.Material) == 0);
            Assert.IsTrue(resourceManager.GetNumberOfLoadedResources(ResourceType.Mesh) == 0);
            Assert.IsTrue(resourceManager.GetNumberOfLoadedResources(ResourceType.Texture) == 0);
        }
    }
}
