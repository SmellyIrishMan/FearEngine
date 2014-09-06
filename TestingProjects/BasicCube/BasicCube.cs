using FearEngine.Meshes;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using System;
using Buffer = SharpDX.Direct3D11.Buffer;
using FearEngine.Resources;
using FearEngine.Resources.Meshes;
using FearEngine.Logger;

namespace BasicCube
{
    class BasicCube : FearEngine.FearEngineApp
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BasicCube app = new BasicCube();
            app.Initialise();
            app.Run();
            app.Shutdown();
        }

        MeshReader meshReader;
        Mesh cube;
        MeshRenderer meshRenderer;
        Material material;

        public override void Initialise()
        {
            Initialise("Basic Cube");

            meshReader = new MeshReader();
            meshRenderer = new MeshRenderer();
            cube = meshReader.LoadMesh("C:\\Users\\Andy\\Documents\\Coding\\Visual Studio 2012\\Projects\\FearEngine\\Resources\\Models\\Box.DAE");

            material = ResourceManager.GetMaterial("TexturedLit");
        }

        protected override void Update()
        {
            m_Context.ClearRenderTargetView(m_RenderTargetView, SharpDX.Color.BurlyWood);
            m_Context.ClearDepthStencilView(m_DepthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);


            meshRenderer.RenderMesh(cube, material);

            m_SwapChain.Present(0, PresentFlags.None);

            base.Update();
        }

        protected override void Shutdown()
        {
            base.Shutdown();
        }
    }
}
