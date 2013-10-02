using FearEngine.Meshes;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Buffer = SharpDX.Direct3D11.Buffer;
using FearEngine.Meshes.Terrain;
using System.Drawing;
using System.IO;
using FearEngine.Resources;
using FearEngine.Logger;

namespace BasicPlane
{
    class BasicPlane : FearEngine.FearEngineApp
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BasicPlane app = new BasicPlane();
            app.Initialise();
            app.Run();
            app.Shutdown();
        }

        CompilationResult m_ShaderByteCode;

        Effect m_ColorEffect;
        EffectTechnique m_ColorTech;

        Effect m_NormalsEffect;
        EffectTechnique m_NormalsTech;

        InputLayout m_Layout;

        Terrain terrain;

        public override void Initialise()
        {
            Initialise("Basic Plane");

            terrain = new Terrain(new Point(256, 256), ResourceManager.GetImage("TEX_SimpleHeightBmp"));
            terrain.Initialise();
            terrain.AddMaterial(ResourceManager.GetMaterial("TexturedLit"));
            terrain.AddMaterial(ResourceManager.GetMaterial("DrawNormals"));
        }

        protected override void Update()
        {
            m_Context.ClearRenderTargetView(m_RenderTargetView, SharpDX.Color.BurlyWood);
            m_Context.ClearDepthStencilView(m_DepthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);

            terrain.SetMaterial("TexturedLit");
            terrain.Render();
            terrain.SetMaterial("DrawNormals");
            terrain.Render();

            m_SwapChain.Present(0, PresentFlags.None);

            base.Update();
        }

        protected override void Shutdown()
        {
            // Release all resources
            m_ShaderByteCode.Dispose();
            m_ColorTech.Dispose();
            m_ColorEffect.Dispose();
            m_NormalsTech.Dispose();
            m_NormalsEffect.Dispose();
            m_Layout.Dispose();

            base.Shutdown();
        }
    }
}
