using FearEngine.Cameras;
using FearEngine.Lighting;
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

            SetupLights(material);

            material.Apply();

            device.SetVertexBuffer(0, mesh.GetVertexBuffer(), mesh.GetVertexStride());
            device.SetVertexInputLayout(mesh.GetInputLayout());

            device.SetIndexBuffer(mesh.GetIndexBuffer(), true);

            device.DrawIndexed(PrimitiveType.TriangleList, mesh.GetIndexBuffer().ElementCount);
        }

        private void SetupLights(Material material)
        {
            LightTypes.DirectionalLight testLight = new LightTypes.DirectionalLight();

            testLight.Ambient = new SharpDX.Vector4(0.05f, 0.05f, 0.05f, 0.0f);
            testLight.Diffuse = new SharpDX.Vector4(0.05f, 0.05f, 0.05f, 0.0f);
            testLight.Specular = new SharpDX.Vector4(0.05f, 0.05f, 0.05f, 0.0f);
            testLight.Direction = new SharpDX.Vector3(-0.05f, -0.05f, -0.05f);
            testLight.pad = 0;
            
            material.SetParameterValue("gDirLight", testLight);
        }
    }
}
