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
    class SimpleSceneDemo : FearEngine.FearGame
    {
        FearEngineImpl fearEngine;

        GameObject teapot;
        Scene scene;

        public void Startup(FearEngineImpl engine)
        {
            fearEngine = engine;

            scene = new Scene(new MeshRenderer(engine.GetDevice()), fearEngine.GetMainCamera());

            teapot = new GameObject("CreateASceneTeapot");
            teapot.AddUpdatable(new ContinuousRotationAroundY(0.001f));

            Mesh mesh = fearEngine.GetResourceManager().GetMesh("TEAPOT");
            Material material = fearEngine.GetResourceManager().GetMaterial("NormalLit");

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
