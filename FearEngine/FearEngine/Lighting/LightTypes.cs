namespace FearEngine.Lighting
{
    public static class LightTypes
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
}
