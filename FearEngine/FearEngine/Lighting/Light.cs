using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FearEngine.Lighting
{
    public interface Light
    {
        Vector3 Direction { get; }
        ValueType LightData { get; }
    }
}
