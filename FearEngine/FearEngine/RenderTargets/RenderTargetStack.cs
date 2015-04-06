using FearEngine.HelperClasses;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;

namespace FearEngine.RenderTargets
{
    public class RenderTargetStack
    {
        Stack<RenderTarget> renderTargetStack;
        GraphicsDevice device;

        public string CurrentRenderTargetName { get { return renderTargetStack.Peek().Name; } }

        public RenderTargetStack(GraphicsDevice dev)
        {
            device = dev;
            renderTargetStack = new Stack<RenderTarget>();

            StoreCurrentRenderTarget();
        }

        private void StoreCurrentRenderTarget()
        {
            RenderTargetView renderTarget = null;
            DepthStencilView depthStencil = null;
            RenderTargetView[] rtvs = device.GetRenderTargets(out depthStencil);

            if (rtvs.Length > 0)
            {
                renderTarget = device.GetRenderTargets(out depthStencil)[0];
            }
            else if (device.BackBuffer != null)
            {
                renderTarget = new RenderTargetView(device, device.BackBuffer);
                depthStencil = new DepthStencilView(device, device.DepthStencilBuffer);
            }
            
            ViewportF viewport = device.GetViewport(0);

            RenderTarget currentRt = new RenderTarget("InitialRT", renderTarget, depthStencil, viewport);
            renderTargetStack.Push(currentRt);
        }

        public void PushRenderTargetAndSwitch(RenderTarget target)
        {
            renderTargetStack.Push(target);
            SwitchToTopRenderTarget();
        }

        private void SwitchToTopRenderTarget()
        {
            RenderTarget target = renderTargetStack.Peek();
            device.SetViewport(target.Viewport);
            device.SetRenderTargets(target.DepthStencil, target.RenderTargetView);

            if (target.DepthStencil != null)
            {
                device.Clear(target.DepthStencil, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1.0f, 0);
            }
            if (target.RenderTargetView != null)
            {
                device.Clear(target.RenderTargetView, new SharpDX.Color4(SRGBLinearConverter.SRGBtoLinear(0.2f), 0.0f, SRGBLinearConverter.SRGBtoLinear(0.2f), 1.0f));
            }
        }

        public void PopRenderTargetAndSwitch()
        {
            renderTargetStack.Pop();
            SwitchToTopRenderTarget();
        }
    }
}
