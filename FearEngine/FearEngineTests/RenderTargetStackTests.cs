using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine.RenderTargets;
using SharpDX.Toolkit.Graphics;
using SharpDX.Direct3D11;

namespace FearEngineTests
{
    [TestClass]
    public class RenderTargetStackTests
    {
        [TestMethod]
        public void CreateARenderTargetStackAndSaveCurrentRT()
        {
            //Given
            GraphicsDevice device = GraphicsDevice.New(DeviceCreationFlags.Debug);

            //When
            RenderTargetStack stack = new RenderTargetStack(device);

            //Then
            Assert.IsTrue(stack.CurrentRenderTargetName.CompareTo("InitialRT") == 0);
        }
    }
}
