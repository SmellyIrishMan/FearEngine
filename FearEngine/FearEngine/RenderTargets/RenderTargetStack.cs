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
            DepthStencilView depthStencil;

            RenderTargetView[] rtvs = device.GetRenderTargets(out depthStencil);
            RenderTargetView renderTarget = null;
            if (rtvs.Length > 0)
            {
                renderTarget = device.GetRenderTargets(out depthStencil)[0];
            }
            
            ViewportF viewport = device.GetViewport(0);

            RenderTarget currentRt = new RenderTarget("InitialRT", renderTarget, depthStencil, viewport);
            renderTargetStack.Push(currentRt);
        }

        public void PushRenderTarget(RenderTarget target)
        {
            target = SwitchRenderTarget(target);

            renderTargetStack.Push(target);
        }

        private RenderTarget SwitchRenderTarget(RenderTarget target)
        {
            device.SetViewport(target.Viewport);
            device.SetRenderTargets(target.DepthStencil, target.RenderTargetView);

            device.Clear(target.DepthStencil, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1.0f, 0);
            device.Clear(new SharpDX.Color4(SRGBLinearConverter.SRGBtoLinear(0.2f), 0.0f, SRGBLinearConverter.SRGBtoLinear(0.2f), 1.0f));
            return target;
        }

        public void PopRenderTargetAndSwitch()
        {
            renderTargetStack.Pop();

            SwitchRenderTarget(renderTargetStack.Peek());
        }
    }
}
