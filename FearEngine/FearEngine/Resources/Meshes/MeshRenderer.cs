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

namespace FearEngine.Resources.Meshes
{
    public class MeshRenderer
    {
        public MeshRenderer()
        {}

        public void RenderMesh(Mesh mesh, Material material)
        {
            for (int pass = 0; pass < material.RenderTechnique.Passes.Count; pass++)
            {
                Matrix world = Matrix.Identity;
                Matrix view = FearEngineApp.MainCamera.View;
                Matrix proj = FearEngineApp.MainCamera.Projection;
                Matrix WVP = world * view * proj;

                material.RenderEffect.Parameters["gWorldViewProj"].SetValue(WVP);

                //TODO Change this all up so that we do it better... yeah
                if (material.Name.CompareTo("TexturedLit") == 0)
                {
                    material.RenderEffect.Parameters["gLightAmbient"].SetValue(new Vector4(0.05f, 0.05f, 0.05f, 0.0f));
                    material.RenderEffect.Parameters["gLightDiffuse"].SetValue(new Vector4(0.05f, 0.05f, 0.05f, 0.0f));
                    material.RenderEffect.Parameters["gLightDir"].SetValue(new Vector4(0.05f, 0.05f, 0.05f, 0.0f));
                }

                material.RenderEffect.CurrentTechnique.Passes[pass].Apply();

                FearEngineApp.GetContext().InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
                FearEngineApp.GetContext().InputAssembler.SetVertexBuffers(0, new SharpDX.Direct3D11.VertexBufferBinding(mesh.GetVertexBuffer(), VertexLayouts.PositionNormalTexture.GetByteSize(), 0));
                FearEngineApp.GetContext().InputAssembler.SetIndexBuffer(mesh.GetIndexBuffer(), Format.R32_UInt, 0);
                FearEngineApp.GetContext().DrawIndexed((int)mesh.GetIndexCount(), 0, 0);
            }
        }
    }
}
