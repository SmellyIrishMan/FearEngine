using FearEngine.RenderTargets;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.Toolkit.Graphics;

namespace FearEngine.Shadows
{
    public class ShadowMap
    {
        private SharpDX.Viewport viewport;

        private DepthStencilView depthMapDSV;
        private ShaderResourceView depthMapSRV;

        private RenderTarget renderTarget;

        public ShaderResourceView ResourceView { get { return depthMapSRV; } }
        public RenderTarget RenderTarget { get { return renderTarget; } }

        private int mipLevels = 1; //Mips will screw with the readings from the depth texture.

        public ShadowMap(GraphicsDevice device, int width, int height)
        {
            viewport = new SharpDX.Viewport(0, 0, width, height, 0.0f, 1.0f);

            Texture2DDescription depthTexDesc = FillOutDepthTextureDescription();
            SharpDX.Direct3D11.Texture2D depthTexture = new SharpDX.Direct3D11.Texture2D(device, depthTexDesc);

            DepthStencilViewDescription dsvDesc = FillOutDSVDescription();
            depthMapDSV = new DepthStencilView(device, depthTexture, dsvDesc);

            ShaderResourceViewDescription srvDesc = FillOutSRVDescription();
            depthMapSRV = new ShaderResourceView(device, depthTexture, srvDesc);

            renderTarget = new RenderTarget("ShadowMap", null, depthMapDSV, viewport);

            depthTexture.Dispose();
        }

        private Texture2DDescription FillOutDepthTextureDescription()
        {
            Texture2DDescription desc;
            desc.Width = viewport.Width;
            desc.Height = viewport.Height;
            desc.MipLevels = mipLevels;
            desc.ArraySize = 1;
            desc.Format = SharpDX.DXGI.Format.R24G8_Typeless;
            desc.SampleDescription.Count = 1;
            desc.SampleDescription.Quality = 0;
            desc.Usage = SharpDX.Direct3D11.ResourceUsage.Default;
            desc.BindFlags = SharpDX.Direct3D11.BindFlags.DepthStencil | SharpDX.Direct3D11.BindFlags.ShaderResource;
            desc.CpuAccessFlags = SharpDX.Direct3D11.CpuAccessFlags.None;
            desc.OptionFlags = SharpDX.Direct3D11.ResourceOptionFlags.None;

            return desc;
        }

        private DepthStencilViewDescription FillOutDSVDescription()
        {
            DepthStencilViewDescription desc = new DepthStencilViewDescription();
            desc.Flags = DepthStencilViewFlags.None;
            desc.Format = SharpDX.DXGI.Format.D24_UNorm_S8_UInt;
            desc.Dimension = DepthStencilViewDimension.Texture2D;
            desc.Texture2D.MipSlice = 0;
            return desc;
        }

        private ShaderResourceViewDescription FillOutSRVDescription()
        {
            ShaderResourceViewDescription desc = new ShaderResourceViewDescription();
            desc.Format = SharpDX.DXGI.Format.R24_UNorm_X8_Typeless;
            desc.Dimension = ShaderResourceViewDimension.Texture2D;
            desc.Texture2D.MipLevels = mipLevels;
            desc.Texture2D.MostDetailedMip = 0;
            return desc;
        }

        public void Dispose()
        {
            depthMapSRV.Dispose();
            depthMapDSV.Dispose();
        }
    }
}
