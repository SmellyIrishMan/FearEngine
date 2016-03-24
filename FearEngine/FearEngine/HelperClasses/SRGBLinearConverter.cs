using System;

namespace FearEngine.HelperClasses
{
    public class SRGBLinearConverter
    {
        private SRGBLinearConverter(){}

        private static float assumedGamma = 2.2f;

        public static float SRGBtoLinear(float sRGB)
        {
            return (float)Math.Pow(sRGB, assumedGamma);
        }

        public static float LinearToSRGB(float linear)
        {
            return (float)Math.Pow(linear, 1 / assumedGamma);
        }
    }
}
