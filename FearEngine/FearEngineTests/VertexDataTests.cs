using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine.Resources.Meshes;

namespace FearEngineTests
{
    [TestClass]
    public class VertexDataTests
    {
        [TestMethod]
        public void SetValueForInput()
        {
            //Given
            VertexData data = new VertexData(new System.Collections.Generic.List<VertexInfoType>(new VertexInfoType[] { VertexInfoType.POSITION }));
            SharpDX.Vector3 newPos = new SharpDX.Vector3(1.0f, 0.0f, 3.0f);
            
            //When
            data.SetValue(VertexInfoType.POSITION, newPos);

            //Then
            Assert.AreEqual(data.GetValue(VertexInfoType.POSITION), newPos);
        }

        [TestMethod]
        public void SetValueForInputThatDoesNotExist()
        {
            //Given
            VertexData data = new VertexData(new System.Collections.Generic.List<VertexInfoType>(new VertexInfoType[] { VertexInfoType.POSITION }));

            //When
            data.SetValue(VertexInfoType.NORMAL, new SharpDX.Vector3(1.0f, 0.0f, 0.0f));

            //Then
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GetValueForInputThatDoesNotExist()
        {
            //Given
            VertexData data = new VertexData(new System.Collections.Generic.List<VertexInfoType>(new VertexInfoType[] { VertexInfoType.POSITION }));

            //When
            SharpDX.Vector3 test = data.GetValue(VertexInfoType.NORMAL);

            //Then
            Assert.AreEqual(test, SharpDX.Vector3.Zero);
        }
    }
}
