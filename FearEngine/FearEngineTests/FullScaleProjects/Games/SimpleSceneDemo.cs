using FearEngine;
using FearEngine.GameObjects;
using FearEngine.GameObjects.Updateables;
using FearEngine.Resources.Materials;
using FearEngine.Resources.Meshes;
using FearEngine.Scene;
using SharpDX.Toolkit;
namespace FearEngineTests.FullScaleProjects.Games
{
    class SimpleSceneDemo : FearEngine.FearGame
    {
        GameObject teapot;
        Scene scene;

        public void Startup(FearEngineImpl engine)
        {
            scene = engine.CreateEmptyScene();

            teapot = new GameObject("Teapot");
            teapot.AddUpdatable(new ContinuousRotationAroundY());

            Mesh mesh = engine.GetResourceManager().GetMesh("TEAPOT");
            Material material = engine.GetResourceManager().GetMaterial("NormalLit");

            SceneObject litTeapot = new SceneObject(teapot, mesh, material);

            scene.AddSceneObject(litTeapot);
        }

        public void Update(GameTime gameTime)
        {
            teapot.Update(gameTime);
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
