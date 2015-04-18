using FearEngine;
using FearEngine.GameObjects;
using FearEngine.GameObjects.Updateables;
using FearEngine.Resources.Materials;
using FearEngine.Resources.Meshes;
using FearEngine.Scenes;
using FearEngine.Lighting;
using FearEngine.Timer;

namespace FearEngineTests.FullScaleProjects.Games
{
    class SimpleSceneDemo : FearEngine.FearGame
    {
        GameObject teapot;
        GameObject cam;

        Scene scene;

        public void Startup(FearEngineImpl engine)
        {
            cam = engine.GameObjectFactory.CreateGameObject("Camera");

            scene = engine.SceneFactory.CreateSceneWithSingleLight(
                engine.CameraFactory.CreateDebugCamera(cam),
                new DirectionalLight());

            teapot = new BaseGameObject("Teapot");
            teapot.AddUpdatable(new ContinuousRotationAroundY(teapot.Transform));

            Mesh mesh = engine.Resources.GetMesh("TEAPOT");
            Material material = engine.Resources.GetMaterial("NormalLit");

            SceneObject litTeapot = new SceneObject(teapot, mesh, material);

            scene.AddSceneObject(litTeapot);
        }

        public void Update(GameTimer gameTime)
        {
            teapot.Update(gameTime);
            cam.Update(gameTime);
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
