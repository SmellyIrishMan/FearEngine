using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine.Shadows;
using SharpDX.Toolkit.Graphics;

namespace FearEngineTests.ShadowMapping
{
    [TestClass]
    public class DepthTextureCreation
    {
        [TestMethod]
        public void CreateBasicShadowMap()
        {
            ShadowMap map = new ShadowMap(GraphicsDevice.New(), 1, 1);

            map.Dispose();

            Assert.IsTrue(true);
        }
    }
}
