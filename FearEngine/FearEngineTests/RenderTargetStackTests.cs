using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine.RenderTargets;
using SharpDX.Toolkit.Graphics;

namespace FearEngineTests
{
    [TestClass]
    public class RenderTargetStackTests
    {
        [TestMethod]
        public void CreateARenderTargetStackAndSaveCurrentRT()
        {
            //Given
            GraphicsDevice device = GraphicsDevice.New();

            //When
            RenderTargetStack stack = new RenderTargetStack(device);

            //Then
            Assert.IsTrue(stack.CurrentRenderTargetName.CompareTo("InitialRT") == 0);
        }
    }
}
