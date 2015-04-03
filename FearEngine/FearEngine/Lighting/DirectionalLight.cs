using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FearEngine.Lighting
{
    public struct DirectionalLight
    {
        public SharpDX.Vector4 Ambient;
        public SharpDX.Vector4 Diffuse;
        public SharpDX.Vector4 Specular;
        public SharpDX.Vector3 Direction;
        public float pad;
    };
}
