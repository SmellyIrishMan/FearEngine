using FearEngine;
using FearEngine.GameObjects;
using FearEngine.Resources;
using FearEngine.Resources.Meshes;
using FearEngine.Scene;
using SharpDX.Toolkit;

namespace FearEngineTests.FullScaleProjects.Games
{
    public class TeapotDemo : FearEngine.FearGame
    {
        Scene scene;

        public void Startup(FearEngineImpl engine)
        {
            scene = new Scene(new MeshRenderer(engine.GetDevice()), engine.GetMainCamera());

            GameObject teapot = new GameObject("Teapot");
            Mesh mesh = engine.GetResourceManager().GetMesh("TEAPOT");
            Material material = engine.GetResourceManager().GetMaterial("NormalLit");

            SceneObject litCube = new SceneObject(teapot, mesh, material);

            scene.AddSceneObject(litCube);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime)
        {
            scene.Render( gameTime );
        }

        public void Shutdown()
        {
        }
    }
}