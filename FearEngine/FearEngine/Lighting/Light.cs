using SharpDX;
using System;

namespace FearEngine.Lighting
{
    public interface Light
    {
        Vector3 Direction { get; }
        ValueType LightData { get; }
    }
}
