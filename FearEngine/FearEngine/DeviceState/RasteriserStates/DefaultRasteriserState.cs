using SharpDX.Toolkit.Graphics;

namespace FearEngine.DeviceState
{
    public class DefaultRasteriserState : RasteriserState
    {
        GraphicsDevice device;
        RasterizerState state;
        public RasterizerState State { get { return GetState(); } }

        public DefaultRasteriserState(FearGraphicsDevice dev)
        {
            device = dev.Device;
            state = null;
        }

        private SharpDX.Toolkit.Graphics.RasterizerState GetState()
        {
            if (state == null)
            {
                state = SharpDX.Toolkit.Graphics.RasterizerState.New(device, device.RasterizerStates.Default);
            }

            return state;
        }
    }
}
