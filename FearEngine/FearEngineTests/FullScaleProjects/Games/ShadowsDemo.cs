using FearEngine;
using FearEngine.GameObjects;
using FearEngine.Resources;
using FearEngine.Resources.Materials;
using FearEngine.Resources.Meshes;
using FearEngine.Scene;
using SharpDX.Toolkit;
namespace FearEngineTests.FullScaleProjects.Games
{
    class ShadowsDemo : FearEngine.FearGame
    {
        Scene scene;

        public void Startup(FearEngineImpl engine)
        {
            scene = engine.CreateEmptyScene();

            GameObject teapot = new BaseGameObject("Teapot");
            Mesh mesh = engine.GetResourceManager().GetMesh("TEAPOT");
            Material material = engine.GetResourceManager().GetMaterial("ShadowTesting");
            SceneObject shadowedTeapot = new SceneObject(teapot, mesh, material);
            scene.AddSceneObject(shadowedTeapot);

            GameObject planeObj = new BaseGameObject("Plane");
            planeObj.Transform.SetScale(3.0f);
            Mesh plane = engine.GetResourceManager().GetMesh("PLANE");
            SceneObject shadowedPlane = new SceneObject(planeObj, plane, material);
            scene.AddSceneObject(shadowedPlane);

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
