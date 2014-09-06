using FearEngine.Time;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using System;
using System.Windows.Forms;
using Device = SharpDX.Direct3D11.Device;
using FearEngine.Cameras;
using System.Threading;
using FearEngine.Resources;
using FearEngine.Logger;

namespace FearEngine
{
    public class FearEngineApp
    {
        protected RenderForm m_Form;
        protected SwapChain m_SwapChain;
        protected SwapChainDescription m_SwapChainDesc;

        private static Device m_Device;
        public static Device Device { get { return m_Device; } protected set { m_Device = value; } }
        protected static DeviceContext m_Context;
        public static DeviceContext Context { get { return m_Context; } }

        protected Factory m_Factory;
        protected Texture2D m_BackBuffer;
        protected Texture2D m_DepthStencilBuffer;
        protected RenderTargetView m_RenderTargetView;
        protected DepthStencilView m_DepthStencilView;

        public static PresentationProperties PresentationProps { get; private set; }

        public static Camera MainCamera;

        public virtual void Initialise()
        {
            Initialise("Fear Engine V1.0");
        }

        protected void Initialise(string title) 
        {
            FearLog.Initialise();
            ResourceManager.Initialise();
            TimeKeeper.Initialise();

            PresentationProps = new PresentationProperties();

            m_Form = new RenderForm(title);
            m_Form.ClientSize = PresentationProps.WindowSize;

            // SwapChain description
            m_SwapChainDesc = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription = new ModeDescription(PresentationProps.Width, PresentationProps.Height, new Rational(60, 1), Format.R8G8B8A8_UNorm_SRgb),
                IsWindowed = !PresentationProps.Fullscreen,
                OutputHandle = m_Form.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            // Create Device and SwapChain
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, m_SwapChainDesc, out m_Device, out m_SwapChain);
            m_Context = Device.ImmediateContext;

            // Ignore all windows events
            m_Factory = m_SwapChain.GetParent<Factory>();
            m_Factory.MakeWindowAssociation(m_Form.Handle, WindowAssociationFlags.IgnoreAll);

            // New RenderTargetView from the backbuffer
            m_BackBuffer = Texture2D.FromSwapChain<Texture2D>(m_SwapChain, 0);
            m_RenderTargetView = new RenderTargetView(Device, m_BackBuffer);

            Texture2DDescription depthDesc = new Texture2DDescription();
            depthDesc.Format = Format.D24_UNorm_S8_UInt;
            depthDesc.ArraySize = 1;
            depthDesc.MipLevels = 1;
            depthDesc.Width = PresentationProps.Width;
            depthDesc.Height = PresentationProps.Height;
            depthDesc.SampleDescription = new SampleDescription(1, 0);
            depthDesc.BindFlags = BindFlags.DepthStencil;
            m_DepthStencilBuffer = new Texture2D(Device, depthDesc);
            m_DepthStencilView = new DepthStencilView(Device, m_DepthStencilBuffer);

            m_Context.Rasterizer.SetViewports(new Viewport(0, 0, PresentationProps.Width, PresentationProps.Height, 0.0f, 1.0f));
            m_Context.OutputMerger.SetTargets(m_DepthStencilView, m_RenderTargetView);

            m_Form.UserResized += OnUserResized;

            InputManager.Initialise(m_Form);
            InputManager.KeyUp += OnKeyUp;

            MainCamera = new Camera();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F)
            {
                PresentationProps.Fullscreen = !PresentationProps.Fullscreen;
                m_SwapChain.SetFullscreenState(PresentationProps.Fullscreen, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                m_Form.Close();
            }
        }

        private void OnUserResized(object sender, EventArgs e)
        {
            RenderForm form = (RenderForm)sender;
            PresentationProps.ChangeWindowSize(form.ClientSize.Width, form.ClientSize.Height);

            // Dispose all previous allocated resources
            ComObject.Dispose(ref m_BackBuffer);
            ComObject.Dispose(ref m_RenderTargetView);

            // Resize the backbuffer
            m_SwapChain.ResizeBuffers(m_SwapChainDesc.BufferCount, PresentationProps.Width, PresentationProps.Height, Format.Unknown, SwapChainFlags.None);

            // Get the backbuffer from the swapchain
            m_BackBuffer = Texture2D.FromSwapChain<Texture2D>(m_SwapChain, 0);
            m_RenderTargetView = new RenderTargetView(Device, m_BackBuffer);

            // Setup targets and viewport for rendering
            m_Context.Rasterizer.SetViewports(new Viewport(0, 0, PresentationProps.Width, PresentationProps.Height, 0.0f, 1.0f));
            m_Context.OutputMerger.SetTargets(m_RenderTargetView);
        }

        protected void Run()
        {
            // Main loop
            RenderLoop.Run(m_Form, () =>
            {
                Update();
            });
        }

        protected virtual void Update()
        {
            TimeKeeper.Update();
            InputManager.Update();
            MainCamera.Update();
            Thread.Sleep(TimeKeeper.FIXED_TIME_STEP);
        }

        private void Dispose()
        {
            m_Factory.Dispose();
            m_BackBuffer.Dispose();

            m_DepthStencilBuffer.Dispose();
            m_DepthStencilView.Dispose();
            m_RenderTargetView.Dispose();

            m_Context.ClearState();
            m_Context.Flush();
            m_Context.Dispose();

            Device.Dispose();
            m_SwapChain.Dispose();
        }

        protected virtual void Shutdown()
        {
            Dispose();

            ResourceManager.Shutdown();
        }
    }
}
