using FearEngine.Cameras;
using SharpDX;
using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources.Meshes
{
    public class MeshRenderer
    {
        public MeshRenderer()
        {}

        public void RenderMesh(GraphicsDevice device, RenderableMesh mesh, Material material, Camera cam)
        {
            Matrix world = Matrix.Identity;

            Matrix view = cam.View;
            Matrix proj = cam.Projection;
            Matrix WVP = world * view * proj;

            //TODO This should be in the update loop and not here.
            material.SetParameterValue("gWorldViewProj", WVP);

            //TODO Change this all up so that we do it better... yeah
            if (material.Name.CompareTo("NormalLit") == 0)
            {
                material.SetParameterValue("gLightAmbient", new Vector4(0.05f, 0.05f, 0.05f, 0.0f));
                material.SetParameterValue("gLightDiffuse", new Vector4(0.2f, 0.05f, 0.6f, 0.0f));
                material.SetParameterValue("gLightDir", new Vector4(-0.05f, -0.05f, -0.05f, 0.0f));
            }

            material.Apply();

            device.SetVertexBuffer(0, mesh.GetVertexBuffer(), mesh.GetVertexStride());
            device.SetVertexInputLayout(mesh.GetInputLayout());

            device.SetIndexBuffer(mesh.GetIndexBuffer(), true);

            device.DrawIndexed(PrimitiveType.TriangleList, mesh.GetIndexBuffer().ElementCount);
        }
    }
}
