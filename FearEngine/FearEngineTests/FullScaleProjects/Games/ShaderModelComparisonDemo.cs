using FearEngine;
using FearEngine.GameObjects;
using FearEngine.GameObjects.Updateables;
using FearEngine.Resources.Materials;
using FearEngine.Resources.Meshes;
using FearEngine.Scenes;
using FearEngine.Timer;

namespace FearEngineTests.FullScaleProjects.Games
{
    class ShaderModelComparisonDemo : FearEngine.FearGame
    {
        GameObject cam;
        GameObject teapotPhong;
        GameObject teapotPBR;
        Scene scene;

        public void Startup(FearEngineImpl engine)
        {
            cam = engine.GameObjectFactory.CreateGameObject("Camera");

            scene = engine.SceneFactory.CreateSceneWithSingleLight(
                engine.CameraFactory.CreateDebugCamera(cam),
                engine.LightFactory.CreateDirectionalLight());

            float seperation = 6.0f;

            teapotPhong = new BaseGameObject("TeapotPhong");
            teapotPhong.Transform.MoveTo(teapotPhong.Transform.Position + (teapotPhong.Transform.Right * seperation));
            teapotPhong.AddUpdatable(new ContinuousRotationAroundY(teapotPhong.Transform));

            teapotPBR = new BaseGameObject("TeapotPBR");
            teapotPBR.Transform.MoveTo(teapotPhong.Transform.Position + (-teapotPhong.Transform.Right * seperation));
            teapotPBR.AddUpdatable(new ContinuousRotationAroundY(teapotPBR.Transform));

            Mesh mesh = engine.Resources.GetMesh("TEAPOT");

            Material phong = engine.Resources.GetMaterial("NormalLit");
            Material pbr = engine.Resources.GetMaterial("PBR_GGX");

            SceneObject phongTeapot = new SceneObject(teapotPhong, mesh, phong);
            SceneObject pbrTeapot = new SceneObject(teapotPBR, mesh, pbr);

            scene.AddSceneObject(phongTeapot);
            scene.AddSceneObject(pbrTeapot);
        }

        public void Update(GameTimer gameTime)
        {
            cam.Update(gameTime);
            teapotPhong.Update( gameTime );
            teapotPBR.Update( gameTime );
        }

        public void Draw(GameTimer gameTime)
        {
            scene.Render(gameTime);
        }

        public void Shutdown()
        {
        }
    }
}
