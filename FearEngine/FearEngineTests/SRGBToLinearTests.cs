using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine.HelperClasses;

namespace FearEngineTests
{
    [TestClass]
    public class SRGBToLinearTests
    {
        private float epsilon = 0.008f;
        [TestMethod]
        public void ConvertSRGBToLinear()
        {
            //Given
            float[] sRGBValues = new float[] { 0.23f, 0.25f, 0.39f, 0.42f, 0.51f, 0.53f, 0.57f, 0.6f, 0.65f, 0.72f, 0.76f, 0.85f, 0.9f, 0.95f };
            float[] linearValues = new float[sRGBValues.Length];

            //When
            for( int i = 0; i < sRGBValues.Length; i++ )
            {
                linearValues[i] = SRGBLinearConverter.SRGBtoLinear(sRGBValues[i]);
            }

            //Then
            float[] expectedValues = new float[] { 0.04f, 0.05f, 0.13f, 0.15f, 0.23f, 0.25f, 0.29f, 0.33f, 0.39f, 0.49f, 0.55f, 0.7f, 0.79f, 0.89f };
            for (int i = 0; i < linearValues.Length; i++)
            {
                float diff = Math.Abs(linearValues[i] - expectedValues[i]);
                Assert.IsTrue(diff < epsilon);
            }
        }

        [TestMethod]
        public void ConvertLinearToSRGB()
        {
            //Given
            float[] linearValues = new float[] { 0.04f, 0.05f, 0.13f, 0.15f, 0.23f, 0.25f, 0.29f, 0.33f, 0.39f, 0.49f, 0.55f, 0.7f, 0.79f, 0.89f };
            float[] sRGBValues = new float[linearValues.Length];

            //When
            for (int i = 0; i < linearValues.Length; i++)
            {
                sRGBValues[i] = SRGBLinearConverter.LinearToSRGB(linearValues[i]);
            }

            //Then
            float[] expectedValues = new float[] { 0.23f, 0.25f, 0.39f, 0.42f, 0.51f, 0.53f, 0.57f, 0.6f, 0.65f, 0.72f, 0.76f, 0.85f, 0.9f, 0.95f };
            for (int i = 0; i < sRGBValues.Length; i++)
            {
                float diff = Math.Abs(sRGBValues[i] - expectedValues[i]);
                Assert.IsTrue(diff < epsilon);
            }
        }
    }
}
