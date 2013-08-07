using FearEngine.Time;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;

namespace FearEngine
{
    public class FearEngineApp
    {
        protected RenderForm m_Form;
        protected SwapChain m_SwapChain;
        protected SwapChainDescription m_SwapChainDesc;

        protected static Device m_Device;
        public static Device Device { get { return m_Device; } }
        protected static DeviceContext m_Context;
        public static DeviceContext Context { get { return m_Context; } }

        protected Factory m_Factory;
        protected Texture2D m_BackBuffer;
        protected RenderTargetView m_RenderTargetView;

        public static PresentationProperties PresentationProps { get; private set; }

        public virtual void Initialise()
        {
            Initialise("Fear Engine V1.0");
        }

        protected void Initialise(string title) 
        {
            TimeKeeper.Initialise();

            PresentationProps = new PresentationProperties();

            m_Form = new RenderForm(title);
            m_Form.ClientSize = PresentationProps.WindowSize;

            // SwapChain description
            m_SwapChainDesc = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription = new ModeDescription(PresentationProps.Width, PresentationProps.Height, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                IsWindowed = !PresentationProps.Fullscreen,
                OutputHandle = m_Form.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            // Create Device and SwapChain
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, m_SwapChainDesc, out m_Device, out m_SwapChain);
            m_Context = m_Device.ImmediateContext;

            // Ignore all windows events
            m_Factory = m_SwapChain.GetParent<Factory>();
            m_Factory.MakeWindowAssociation(m_Form.Handle, WindowAssociationFlags.IgnoreAll);

            // New RenderTargetView from the backbuffer
            m_BackBuffer = Texture2D.FromSwapChain<Texture2D>(m_SwapChain, 0);
            m_RenderTargetView = new RenderTargetView(m_Device, m_BackBuffer);

            m_Context.Rasterizer.SetViewports(new Viewport(0, 0, PresentationProps.Width, PresentationProps.Height, 0.0f, 1.0f));
            m_Context.OutputMerger.SetTargets(m_RenderTargetView);

            m_Form.UserResized += OnUserResized;

            InputManager.Initialise(m_Form);
            InputManager.KeyUp += OnKeyUp;
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
            m_RenderTargetView = new RenderTargetView(m_Device, m_BackBuffer);

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
        }

        protected virtual void Dispose()
        {
            m_Factory.Dispose();
            m_BackBuffer.Dispose();
            m_RenderTargetView.Dispose();

            m_Context.ClearState();
            m_Context.Flush();
            m_Context.Dispose();

            m_Device.Dispose();
            m_SwapChain.Dispose();
        }
    }
}
