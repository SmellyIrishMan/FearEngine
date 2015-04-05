using FearEngine;
using FearEngine.GameObjects;
using FearEngine.Resources;
using FearEngine.Resources.Meshes;
using FearEngine.Scene;
using SharpDX.Toolkit;
namespace FearEngineTests.FullScaleProjects.Games
{
    class ShadowsDemo : FearEngine.FearGame
    {
        FearEngineImpl fearEngine;

        Scene scene;

        public void Startup(FearEngineImpl engine)
        {
            fearEngine = engine;

            scene = new Scene(new MeshRenderer(engine.GetDevice()), fearEngine.GetMainCamera());

            GameObject teapot = new GameObject("Teapot");

            Mesh mesh = fearEngine.GetResourceManager().GetMesh("TEAPOT");
            Material material = fearEngine.GetResourceManager().GetMaterial("NormalLit");

            SceneObject litTeapot = new SceneObject(teapot, mesh, material);
            scene.AddSceneObject(litTeapot);

            scene.EnableShadows();
        }

        public void Update(GameTime gameTime)
        {
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
