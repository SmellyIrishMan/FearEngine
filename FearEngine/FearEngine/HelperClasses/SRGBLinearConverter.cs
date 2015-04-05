using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FearEngine.HelperClasses
{
    public class SRGBLinearConverter
    {
        private SRGBLinearConverter(){}

        private static float assumedGamma = 2.2f;

        public static float SRGBtoLinear(float sRGB)
        {
            return (float)Math.Pow(sRGB, 2.2f);
        }

        public static float LinearToSRGB(float linear)
        {
            return (float)Math.Pow(linear, 1 / 2.2f);
        }
    }
}
