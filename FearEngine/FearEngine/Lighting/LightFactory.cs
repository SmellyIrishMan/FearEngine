namespace FearEngine.Lighting
{
    public class LightFactory
    {
        public LightFactory()
        {

        }

        public DirectionalLight CreateLight()
        {
            DirectionalLight light = new DirectionalLight();

            light.Diffuse = new SharpDX.Vector4(0.8f, 0.8f, 0.8f, 0.0f);
            light.Direction = new SharpDX.Vector3(-0.5f, -1.0f, -0.25f);
            light.Direction.Normalize();
            light.pad = 0;

            return light;
        }
    }
}
