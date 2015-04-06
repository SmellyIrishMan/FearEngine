using SharpDX.Toolkit.Graphics;

namespace FearEngine
{
    public class SharpDXGraphicsDevice : FearGraphicsDevice
    {
        private GraphicsDevice device;

        public SharpDXGraphicsDevice(GraphicsDevice dev)
        {
            device = dev;
        }

        public GraphicsDevice Device
        {
            get { return device; }
        }
    }
}
