using SharpDX.Toolkit.Graphics;

namespace FearEngine
{
    public class SharpDXGraphicsDevice : FearGraphicsDevice
    {
        private GraphicsDevice device;

        public SharpDXGraphicsDevice()
        {
            device = GraphicsDevice.New();
        }

        public GraphicsDevice Device
        {
            get { return device; }
        }
    }
}
