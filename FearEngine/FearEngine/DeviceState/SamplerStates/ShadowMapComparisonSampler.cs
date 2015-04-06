using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FearEngine.DeviceState.SamplerStates
{
    public class ShadowMapComparisonSampler : SamplerState
    {
        GraphicsDevice device;
        SharpDX.Toolkit.Graphics.SamplerState sampler;
        public SharpDX.Toolkit.Graphics.SamplerState State { get { return GetState(); } }

        public ShadowMapComparisonSampler(FearGraphicsDevice dev)
        {
            device = dev.Device;
            sampler = null;
        }

        private SharpDX.Toolkit.Graphics.SamplerState GetState()
        {
            if (sampler == null)
            {
                SharpDX.Direct3D11.SamplerStateDescription desc = new SharpDX.Direct3D11.SamplerStateDescription();
                desc.Filter = SharpDX.Direct3D11.Filter.ComparisonMinMagLinearMipPoint;
                desc.AddressU = SharpDX.Direct3D11.TextureAddressMode.Border;
                desc.AddressV = SharpDX.Direct3D11.TextureAddressMode.Border;
                desc.AddressW = SharpDX.Direct3D11.TextureAddressMode.Border;
                desc.BorderColor = SharpDX.Color4.Black;
                desc.ComparisonFunction = SharpDX.Direct3D11.Comparison.LessEqual;
                sampler = SharpDX.Toolkit.Graphics.SamplerState.New(device, desc);
            }

            return sampler;
        }
    }
}