using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.Toolkit.Graphics;

namespace FearEngine.RenderTargets
{
    public struct RenderTarget
    {
        RenderTargetView renderTargetView;
        DepthStencilView depthStencil;
        ViewportF viewport;

        public string Name { get; private set; }
        public RenderTargetView RenderTargetView { get { return renderTargetView; } }
        public DepthStencilView DepthStencil { get { return depthStencil; } }
        public ViewportF Viewport { get { return viewport; } }

        public RenderTarget(string name, RenderTargetView rt, DepthStencilView ds, ViewportF v) : this()
        {
            renderTargetView = rt;
            depthStencil = ds;
            viewport = v;

            Name = name;
        }
    }
}
