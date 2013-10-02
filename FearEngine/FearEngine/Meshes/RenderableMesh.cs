using FearEngine.Resources;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace FearEngine.Meshes
{
    public abstract class RenderableMesh : GameObject
    {
        protected int VertexCount { get; set; }
        protected int IndexCount { get; set; }

        protected Buffer VertexBuffer { get; set; }
        protected Buffer IndexBuffer { get; set; }

        Dictionary<string, Material> Materials{get; set;}
        Material CurrentMaterial{get; set;}

        public RenderableMesh()
        {
            Materials = new Dictionary<string, Material>();
        }

        public void SetGeometryBuffers()
        {
        }

        public void AddMaterial(Material mat)
        {
            Materials.Add(mat.Name, mat);
        }

        public void SetMaterial(string name)
        {
            CurrentMaterial = Materials[name];
        }

        public void Render()
        {
            for (int pass = 0; pass < CurrentMaterial.RenderTechnique.Description.PassCount; pass++)
            {
                Matrix world = Matrix.Identity;
                Matrix view = FearEngineApp.MainCamera.View;
                Matrix proj = FearEngineApp.MainCamera.Projection;
                Matrix WVP = world * view * proj;

                CurrentMaterial.RenderEffect.GetVariableByName("gWorldViewProj").AsMatrix().SetMatrix(WVP);

                //TODO Change this all up so that we do it better... yeah
                if (CurrentMaterial.Name.CompareTo("TexturedLit") == 0)
                {
                    CurrentMaterial.RenderEffect.GetVariableByName("gLightAmbient").AsVector().Set(new Vector4(0.05f, 0.05f, 0.05f, 0.0f));
                    CurrentMaterial.RenderEffect.GetVariableByName("gLightDiffuse").AsVector().Set(new Vector4(0.9f, 0.76f, 0.8f, 0.0f));
                    CurrentMaterial.RenderEffect.GetVariableByName("gLightDir").AsVector().Set(new Vector4(0.2f, -1.0f, 0.15f, 0.0f));
                }

                // Layout from VertexShader input signature
                InputLayout m_Layout = new InputLayout(
                    FearEngineApp.Device,
                    ShaderSignature.GetInputSignature(CurrentMaterial.RenderTechnique.GetPassByIndex(pass).Description.Signature),
                    VertexLayouts.PositionNormalTexture.GetInputElements());
                FearEngineApp.Context.InputAssembler.InputLayout = m_Layout;

                CurrentMaterial.RenderTechnique.GetPassByIndex(pass).Apply(FearEngineApp.Context);

                FearEngineApp.Context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
                FearEngineApp.Context.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(VertexBuffer, VertexLayouts.PositionNormalTexture.GetByteSize(), 0));
                FearEngineApp.Context.InputAssembler.SetIndexBuffer(IndexBuffer, Format.R32_UInt, 0);
                FearEngineApp.Context.DrawIndexed(IndexCount, 0, 0);
            }
        }
    }
}