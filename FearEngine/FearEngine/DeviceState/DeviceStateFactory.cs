using SharpDX.Toolkit.Graphics;
using System.Collections.Generic;

namespace FearEngine.DeviceState
{
    public class DeviceStateFactory
    {
        public enum SamplerStates
        {
            ShadowMapComparison,
        }

        public enum RasterizerStates
        {
            Default,
            ShadowBiasedDepth,
        }

        GraphicsDevice device;

        Dictionary<SamplerStates, SamplerState> samplerStates;
        Dictionary<RasterizerStates, RasterizerState> rasterizerStates;

        public DeviceStateFactory(GraphicsDevice dev)
        {
            device = dev;

            samplerStates = new Dictionary<SamplerStates, SamplerState>();
            rasterizerStates = new Dictionary<RasterizerStates, RasterizerState>();
        }

        public SamplerState CreateSamplerState(SamplerStates state)
        {
            if (samplerStates.ContainsKey(state))
            {
                return samplerStates[state];
            }

            SharpDX.Direct3D11.SamplerStateDescription desc = new SharpDX.Direct3D11.SamplerStateDescription();

            switch (state)
            {
                case SamplerStates.ShadowMapComparison:
                    desc.Filter = SharpDX.Direct3D11.Filter.ComparisonMinMagLinearMipPoint;
                    desc.AddressU = SharpDX.Direct3D11.TextureAddressMode.Border;
                    desc.AddressV = SharpDX.Direct3D11.TextureAddressMode.Border;
                    desc.AddressW = SharpDX.Direct3D11.TextureAddressMode.Border;
                    desc.BorderColor = SharpDX.Color4.Black;
                    desc.ComparisonFunction = SharpDX.Direct3D11.Comparison.LessEqual;
                    break;
            }

            SamplerState newSampler = SamplerState.New(device, desc);
            samplerStates.Add(state, newSampler);
            return newSampler;
        }

        public RasterizerState CreateRasteriserState(RasterizerStates state)
        {
            if (rasterizerStates.ContainsKey(state))
            {
                return rasterizerStates[state];
            }

            SharpDX.Direct3D11.RasterizerStateDescription desc = new SharpDX.Direct3D11.RasterizerStateDescription();
            desc.CullMode = SharpDX.Direct3D11.CullMode.Back;
            desc.DepthBias = 0;
            desc.DepthBiasClamp = 0.0f;
            desc.FillMode = SharpDX.Direct3D11.FillMode.Solid;
            desc.IsAntialiasedLineEnabled = false;
            desc.IsDepthClipEnabled = true;
            desc.IsFrontCounterClockwise = false;
            desc.IsMultisampleEnabled = false;
            desc.IsScissorEnabled = false;
            desc.SlopeScaledDepthBias = 0.0f;

            switch (state)
            {

                case RasterizerStates.Default:
                    break;
                case RasterizerStates.ShadowBiasedDepth:
                    desc.CullMode = SharpDX.Direct3D11.CullMode.None;
                    desc.DepthBias = 100000;
                    desc.DepthBiasClamp = 0.0f;
                    desc.SlopeScaledDepthBias = 1.0f;
                    break;
            }

            RasterizerState newRasteriser = RasterizerState.New(device, desc);
            rasterizerStates.Add(state, newRasteriser);
            return newRasteriser;
        }
    }
}
