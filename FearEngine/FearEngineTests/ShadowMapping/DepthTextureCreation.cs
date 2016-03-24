using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine.Shadows;
using SharpDX.Toolkit.Graphics;
using SharpDX.Direct3D11;

namespace FearEngineTests.ShadowMapping
{
    [TestClass]
    public class DepthTextureCreation
    {
        [TestMethod]
        public void CreateBasicShadowMap()
        {
            //Given
            ShadowMap map = new ShadowMap(GraphicsDevice.New(DeviceCreationFlags.Debug), 1, 1);

            //When

            //Then
            Assert.IsTrue(map.ResourceView != null);
            map.Dispose();
        }
    }
}
