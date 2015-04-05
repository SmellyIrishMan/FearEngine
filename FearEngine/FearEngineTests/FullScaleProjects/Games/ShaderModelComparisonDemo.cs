using FearEngine;
using FearEngine.GameObjects;
using FearEngine.GameObjects.Updateables;
using FearEngine.Lighting;
using FearEngine.Resources;
using FearEngine.Resources.Meshes;
using FearEngine.Scene;
using SharpDX.Toolkit;

namespace FearEngineTests.FullScaleProjects.Games
{
    class ShaderModelComparisonDemo : FearEngine.FearGame
    {
        FearEngineImpl fearEngine;

        GameObject teapotPhong;
        GameObject teapotPBR;
        Scene scene;

        public void Startup(FearEngineImpl engine)
        {
            fearEngine = engine;

            scene = new Scene(null, null, new MeshRenderer(engine.GetDevice()), fearEngine.GetMainCamera());

            float seperation = 6.0f;

            teapotPhong = new GameObject("TeapotPhong");
            teapotPhong.Transform.MoveTo(teapotPhong.Transform.Position + (teapotPhong.Transform.Right * seperation));
            teapotPhong.AddUpdatable(new ContinuousRotationAroundY());

            teapotPBR = new GameObject("TeapotPBR");
            teapotPBR.Transform.MoveTo(teapotPhong.Transform.Position + (-teapotPhong.Transform.Right * seperation));
            teapotPBR.AddUpdatable(new ContinuousRotationAroundY());

            Mesh mesh = fearEngine.GetResourceManager().GetMesh("TEAPOT");

            Material phong = fearEngine.GetResourceManager().GetMaterial("NormalLit");
            Material pbr = fearEngine.GetResourceManager().GetMaterial("PBR_GGX");

            SceneObject phongTeapot = new SceneObject(teapotPhong, mesh, phong);
            SceneObject pbrTeapot = new SceneObject(teapotPBR, mesh, pbr);

            scene.AddSceneObject(phongTeapot);
            scene.AddSceneObject(pbrTeapot);
        }

        public void Update(GameTime gameTime)
        {
            teapotPhong.Update(gameTime);
            teapotPBR.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            scene.Render(gameTime);
        }

        public void Shutdown()
        {
        }
    }
}
