using FearEngine.GameObjects;
using SharpDX;
using System;

namespace FearEngine.Lighting
{
    public class DirectionalLight : TransformAttacher, Light
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

        override sealed protected void OnTransformChanged(Transform newTransform)
        {
            data.Direction = Vector3.Transform(Vector3.ForwardLH, newTransform.Rotation);
            data.Direction.Normalize();
        }
    }
}
