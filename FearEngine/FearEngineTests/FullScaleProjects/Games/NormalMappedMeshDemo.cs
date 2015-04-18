using FearEngine;
using FearEngine.Resources.Meshes;
using FearEngine.GameObjects;
using FearEngine.Scenes;
using FearEngine.Resources.Materials;
using FearEngine.Timer;

namespace FearEngineTests.FullScaleProjects.Games
{
    public class NormalMappedMeshDemo : FearEngine.FearGame
    {
        Scene scene;
        GameObject cam;

        public void Startup(FearEngineImpl engine)
        {
            cam = engine.GameObjectFactory.CreateGameObject("Camera");

            scene = engine.SceneFactory.CreateSceneWithSingleLight(
                engine.CameraFactory.CreateDebugCamera(cam),
                engine.LightFactory.CreateDirectionalLight());

            GameObject teapot = new BaseGameObject("FloorPlane");
            Mesh mesh = engine.Resources.GetMesh("PLANE");
            Material material = engine.Resources.GetMaterial("NormalMapped");
            material.SetParameterResource("gAlbedo", engine.Resources.GetTexture("GravelCobble"));
            material.SetParameterResource("gNormal", engine.Resources.GetTexture("GravelCobbleNormal"));

            //drawNormalsMaterial = fearEngine.Resources.GetMaterial("DrawNormals");

            SceneObject litCube = new SceneObject(teapot, mesh, material);

            scene.AddSceneObject(litCube);
        }

        public void Update(GameTimer gameTime)
        {
            cam.Update(gameTime);
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
