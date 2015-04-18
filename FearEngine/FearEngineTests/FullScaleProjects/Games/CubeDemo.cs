using FearEngine;
using FearEngine.Resources.Meshes;
using FearEngine.Scenes;
using FearEngine.GameObjects;
using FearEngine.Resources.Materials;
using FearEngine.Timer;

namespace FearEngineTests.FullScaleProjects.Games
{
    public class CubeDemo : FearEngine.FearGame
    {
        Scene scene;

        public void Startup(FearEngineImpl engine)
        {
            scene = engine.SceneFactory.CreateEmptyScene(engine.MainCamera);

            GameObject cube = new BaseGameObject("Cube");
            Mesh mesh = engine.Resources.GetMesh("BOX");
            Material material = engine.Resources.GetMaterial("NormalLit");

            SceneObject litCube = new SceneObject(cube, mesh, material);

            scene.AddSceneObject(litCube);
        }

        public void Update(GameTimer gameTime)
        {

        }

        public void Draw(GameTimer gameTime)
        {
            scene.Render( gameTime );
        }

        public void Shutdown()
        {
        }
    }
}
