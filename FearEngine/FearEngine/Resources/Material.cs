using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FearEngine.Resources
{
    public class Material
    {
        public String Name { get; set; }
        public Effect RenderEffect { get; set; }
        public EffectTechnique RenderTechnique { get; set; }
    }
}
