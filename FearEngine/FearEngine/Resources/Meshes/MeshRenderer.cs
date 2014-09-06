using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FearEngine.Resources.Meshes
{
    public class MeshRenderer
    {
        public MeshRenderer()
        {}

        public void RenderMesh(Mesh mesh, Material material)
        {
            for (int pass = 0; pass < material.RenderTechnique.Description.PassCount; pass++)
            {
                Matrix world = Matrix.Identity;
                Matrix view = FearEngineApp.MainCamera.View;
                Matrix proj = FearEngineApp.MainCamera.Projection;
                Matrix WVP = world * view * proj;

                material.RenderEffect.GetVariableByName("gWorldViewProj").AsMatrix().SetMatrix(WVP);

                //TODO Change this all up so that we do it better... yeah
                if (material.Name.CompareTo("TexturedLit") == 0)
                {
                    material.RenderEffect.GetVariableByName("gLightAmbient").AsVector().Set(new Vector4(0.05f, 0.05f, 0.05f, 0.0f));
                    material.RenderEffect.GetVariableByName("gLightDiffuse").AsVector().Set(new Vector4(0.9f, 0.76f, 0.8f, 0.0f));
                    material.RenderEffect.GetVariableByName("gLightDir").AsVector().Set(new Vector4(0.2f, -1.0f, 0.15f, 0.0f));
                }

                // Layout from VertexShader input signature
                //TODO This should really be a once off thing. We don't need to create it on every render call.
                InputLayout m_Layout = new InputLayout(
                    FearEngineApp.Device,
                    ShaderSignature.GetInputSignature(material.RenderTechnique.GetPassByIndex(pass).Description.Signature),
                    VertexLayouts.PositionNormalTexture.GetInputElements());
                FearEngineApp.Context.InputAssembler.InputLayout = m_Layout;

                material.RenderTechnique.GetPassByIndex(pass).Apply(FearEngineApp.Context);

                FearEngineApp.Context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
                FearEngineApp.Context.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(mesh.GetVertexBuffer(), VertexLayouts.PositionNormalTexture.GetByteSize(), 0));
                FearEngineApp.Context.InputAssembler.SetIndexBuffer(mesh.GetIndexBuffer(), Format.R32_UInt, 0);
                FearEngineApp.Context.DrawIndexed((int)mesh.GetIndexCount(), 0, 0);

                //TODO Move this somewhere better.
                m_Layout.Dispose();
            }
        }
    }
}
