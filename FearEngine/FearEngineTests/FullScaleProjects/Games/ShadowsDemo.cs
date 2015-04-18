using FearEngine;
using FearEngine.GameObjects;
using FearEngine.Lighting;
using FearEngine.Resources.Materials;
using FearEngine.Resources.Meshes;
using FearEngine.Scenes;
using FearEngine.Timer;
namespace FearEngineTests.FullScaleProjects.Games
{
    class ShadowsDemo : FearEngine.FearGame
    {
        Scene scene;
        GameObject rotatingLight;

        public void Startup(FearEngineImpl engine)
        {
            rotatingLight = engine.GameObjectFactory.CreateGameObject("RotatingLightFixture");
            rotatingLight.AddUpdatable(engine.UpdateableFactory.CreateContinuousRandomSlerp(0.25f));

            Light light = new FearEngine.Lighting.DirectionalLight();
            TransformAttacher attacher = (TransformAttacher)light;
            attacher.AttactToTransform(rotatingLight.Transform);

            scene = engine.SceneFactory.CreateSceneWithSingleLight(engine.MainCamera, light);

            GameObject teapot = new BaseGameObject("Teapot");
            Mesh mesh = engine.Resources.GetMesh("TEAPOT");
            Material material = engine.Resources.GetMaterial("ShadowedPBR");
            SceneObject shadowedTeapot = new SceneObject(teapot, mesh, material);
            scene.AddSceneObject(shadowedTeapot);

            GameObject planeObj = new BaseGameObject("Plane");
            planeObj.Transform.SetScale(3.0f);
            Mesh plane = engine.Resources.GetMesh("PLANE");
            SceneObject shadowedPlane = new SceneObject(planeObj, plane, material);
            scene.AddSceneObject(shadowedPlane);

            scene.EnableShadows();
        }

        public void Update(GameTimer gameTime)
        {
            rotatingLight.Update(gameTime);
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
