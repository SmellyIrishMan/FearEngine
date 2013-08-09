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
            app.Dispose();
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

            terrain = new Terrain(new Point(256, 256), "../../../../../Resources/Textures/Heightmaps/Heightmap.bmp");
            terrain.Initialise();

            // Compile Vertex and Pixel shaders
            if (!File.Exists("sharpdx_direct3d11_effects_x86.dll"))
            {
                File.Copy("../../../../../ThirdParty/SharpDX/Bin/Signed-net40/sharpdx_direct3d11_effects_x86.dll" ,"sharpdx_direct3d11_effects_x86.dll");
            }
            m_ShaderByteCode = ShaderBytecode.CompileFromFile("../../../../../Resources/Shaders/BasicPositionNormal.fx", "fx_5_0", ShaderFlags.None, EffectFlags.None, null, null);
            m_ColorEffect = new Effect(Device, m_ShaderByteCode, EffectFlags.None);
            m_ColorTech = m_ColorEffect.GetTechniqueByName("BasicPositionNormalTech");

            m_ShaderByteCode = ShaderBytecode.CompileFromFile("../../../../../Resources/Shaders/DrawNormals.fx", "fx_5_0", ShaderFlags.None, EffectFlags.None, null, null);
            m_NormalsEffect = new Effect(Device, m_ShaderByteCode, EffectFlags.None);
            m_NormalsTech = m_NormalsEffect.GetTechniqueByName("DrawNormalsTech");
        }

        protected override void Update()
        {
            m_Context.ClearRenderTargetView(m_RenderTargetView, SharpDX.Color.BurlyWood);
            m_Context.ClearDepthStencilView(m_DepthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);

            for (int pass = 0; pass < m_ColorTech.Description.PassCount; pass++)
            {
                Matrix world = Matrix.Identity;
                Matrix view = MainCamera.View;
                Matrix proj = MainCamera.Projection;
                Matrix WVP = world * view * proj;

                m_ColorEffect.GetVariableByName("gWorldViewProj").AsMatrix().SetMatrix(WVP);

                // Layout from VertexShader input signature
                m_Layout = new InputLayout(
                    Device,
                    ShaderSignature.GetInputSignature(m_ColorTech.GetPassByIndex(pass).Description.Signature),
                    VertexLayouts.PositionNormal.GetInputElements());
                m_Context.InputAssembler.InputLayout = m_Layout;

                m_ColorTech.GetPassByIndex(pass).Apply(m_Context);

                terrain.Render();
            }

            for (int pass = 0; pass < m_NormalsTech.Description.PassCount; pass++)
            {
                Matrix world = Matrix.Identity;
                Matrix view = MainCamera.View;
                Matrix proj = MainCamera.Projection;
                Matrix WVP = world * view * proj;

                m_NormalsEffect.GetVariableByName("gWorldViewProj").AsMatrix().SetMatrix(WVP);

                // Layout from VertexShader input signature
                m_Layout = new InputLayout(
                    Device,
                    ShaderSignature.GetInputSignature(m_NormalsTech.GetPassByIndex(pass).Description.Signature),
                    VertexLayouts.PositionNormal.GetInputElements());
                m_Context.InputAssembler.InputLayout = m_Layout;

                m_NormalsTech.GetPassByIndex(pass).Apply(m_Context);

                terrain.Render();
            }

            m_SwapChain.Present(0, PresentFlags.None);

            base.Update();
        }

        protected override void Dispose()
        {
            // Release all resources
            m_ShaderByteCode.Dispose();
            m_ColorTech.Dispose();
            m_ColorEffect.Dispose();
            m_NormalsTech.Dispose();
            m_NormalsEffect.Dispose();
            m_Layout.Dispose();

            base.Dispose();
        }
    }
}
