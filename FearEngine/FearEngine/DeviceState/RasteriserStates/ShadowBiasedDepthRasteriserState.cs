using SharpDX.Toolkit.Graphics;

namespace FearEngine.DeviceState
{
    public class ShadowBiasedDepthRasteriserState : RasteriserState
    {
        GraphicsDevice device;
        RasterizerState state;
        public RasterizerState State{ get { return GetState(); }}

        public ShadowBiasedDepthRasteriserState(FearGraphicsDevice dev)
        {
            device = dev.Device;
            state = null;
        }

        private SharpDX.Toolkit.Graphics.RasterizerState GetState()
        {
            if (state == null)
            {
                SharpDX.Direct3D11.RasterizerStateDescription desc = new SharpDX.Direct3D11.RasterizerStateDescription();
                desc.CullMode = SharpDX.Direct3D11.CullMode.Front;
                desc.DepthBias = 36000;
                desc.DepthBiasClamp = 0.0f;
                desc.FillMode = SharpDX.Direct3D11.FillMode.Solid;
                desc.IsAntialiasedLineEnabled = false;
                desc.IsDepthClipEnabled = true;
                desc.IsFrontCounterClockwise = false;
                desc.IsMultisampleEnabled = false;
                desc.IsScissorEnabled = false;
                desc.SlopeScaledDepthBias = 1.7f;
                state = SharpDX.Toolkit.Graphics.RasterizerState.New(device, desc);
            }

            return state;
        }
    }
}
