using FearEngine;
using FearEngine.GameObjects;
using FearEngine.GameObjects.Updateables;
using FearEngine.Resources.Materials;
using FearEngine.Resources.Meshes;
using FearEngine.Scenes;
using SharpDX.Toolkit;

namespace FearEngineTests.FullScaleProjects.Games
{
    class ShaderModelComparisonDemo : FearEngine.FearGame
    {
        GameObject teapotPhong;
        GameObject teapotPBR;
        Scene scene;

        public void Startup(FearEngineImpl engine)
        {
            scene = engine.CreateEmptyScene();

            float seperation = 6.0f;

            teapotPhong = new BaseGameObject("TeapotPhong");
            teapotPhong.Transform.MoveTo(teapotPhong.Transform.Position + (teapotPhong.Transform.Right * seperation));
            teapotPhong.AddUpdatable(new ContinuousRotationAroundY());

            teapotPBR = new BaseGameObject("TeapotPBR");
            teapotPBR.Transform.MoveTo(teapotPhong.Transform.Position + (-teapotPhong.Transform.Right * seperation));
            teapotPBR.AddUpdatable(new ContinuousRotationAroundY());

            Mesh mesh = engine.GetResourceManager().GetMesh("TEAPOT");

            Material phong = engine.GetResourceManager().GetMaterial("NormalLit");
            Material pbr = engine.GetResourceManager().GetMaterial("PBR_GGX");

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
