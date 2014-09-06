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
            Matrix world = Matrix.Identity;

            Matrix view = FearEngineApp.MainCamera.View;
            Matrix proj = FearEngineApp.MainCamera.Projection;
            Matrix WVP = world * view * proj;

            //TODO This should be in the update loop and not here.
            material.RenderEffect.Parameters["gWorldViewProj"].SetValue(WVP);

            //TODO Change this all up so that we do it better... yeah
            if (material.Name.CompareTo("NormalLit") == 0)
            {
                material.RenderEffect.Parameters["gLightAmbient"].SetValue(new Vector4(0.05f, 0.05f, 0.05f, 0.0f));
                material.RenderEffect.Parameters["gLightDiffuse"].SetValue(new Vector4(0.05f, 0.05f, 0.05f, 0.0f));
                material.RenderEffect.Parameters["gLightDir"].SetValue(new Vector4(0.05f, 0.05f, 0.05f, 0.0f));
            }
            material.RenderEffect.CurrentTechnique.Passes[0].Apply();

            FearEngineApp.GetDevice().SetVertexBuffer(mesh.GetVertexBuffer());
            FearEngineApp.GetDevice().SetVertexInputLayout(mesh.GetInputLayout());

            FearEngineApp.GetDevice().SetIndexBuffer(mesh.GetIndexBuffer(), true);

            FearEngineApp.GetDevice().DrawIndexed(PrimitiveType.TriangleList, mesh.GetIndexBuffer().ElementCount);
        }
    }
}
