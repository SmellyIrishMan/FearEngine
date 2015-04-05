using SharpDX.Direct3D11;
using SharpDX.Toolkit.Graphics;
using System.Collections.Generic;

namespace FearEngine.DeviceState
{
    public class DeviceStateFactory
    {
        public enum SamplerStates
        {
            ShadowMapComparison
        }

        GraphicsDevice device;

        Dictionary<SamplerStates, SharpDX.Direct3D11.SamplerState> samplerStates;

        public DeviceStateFactory(GraphicsDevice dev)
        {
            device = dev;

            samplerStates = new Dictionary<SamplerStates, SharpDX.Direct3D11.SamplerState>();
        }

        public SharpDX.Direct3D11.SamplerState CreateSamplerState(SamplerStates state)
        {
            if (samplerStates.ContainsKey(state))
            {
                return samplerStates[state];
            }

            SamplerStateDescription desc = new SamplerStateDescription();

            switch (state)
            {
                case SamplerStates.ShadowMapComparison:
                    desc.Filter = Filter.ComparisonMinMagLinearMipPoint;
                    desc.AddressU = TextureAddressMode.Border;
                    desc.AddressV = TextureAddressMode.Border;
                    desc.AddressW = TextureAddressMode.Border;
                    desc.BorderColor = SharpDX.Color4.Black;
                    desc.ComparisonFunction = Comparison.LessEqual;
                    break;
            }

            SharpDX.Direct3D11.SamplerState newSampler = new SharpDX.Direct3D11.SamplerState(device, desc);
            samplerStates.Add(state, newSampler);
            return newSampler;
        }
    }
}
