using FearEngine.Cameras;
using SharpDX;
using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources.Meshes
{
    public class MeshRenderer
    {
        public MeshRenderer()
        {}

        public void RenderMesh(GraphicsDevice device, MeshRenderable mesh, Material material, Camera cam)
        {
            Matrix world = Matrix.Identity;

            Matrix view = cam.View;
            Matrix proj = cam.Projection;
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

            device.SetVertexBuffer(mesh.GetVertexBuffer());
            device.SetVertexInputLayout(mesh.GetInputLayout());

            device.SetIndexBuffer(mesh.GetIndexBuffer(), true);

            device.DrawIndexed(PrimitiveType.TriangleList, mesh.GetIndexBuffer().ElementCount);
        }
    }
}
