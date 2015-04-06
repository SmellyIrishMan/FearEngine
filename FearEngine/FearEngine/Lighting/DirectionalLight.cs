using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FearEngine.Lighting
{
    public class DirectionalLight : Light
    {
        public struct DirectionalLightData
        {
            public SharpDX.Vector4 Diffuse;
            public SharpDX.Vector3 Direction;
            public float pad;
        };

        private DirectionalLightData data;
        public ValueType LightData
        {
            get { return data; }
        }

        public DirectionalLight()
        {
            data = new DirectionalLightData();

            data.Diffuse = new SharpDX.Vector4(0.8f, 0.8f, 0.8f, 0.0f);

            data.Direction = new SharpDX.Vector3(-0.5f, -1.0f, -0.25f);
            data.Direction.Normalize();

            data.pad = 0;
        }

        public SharpDX.Vector3 Direction
        {
            get { return data.Direction; }
        }
    }
}
