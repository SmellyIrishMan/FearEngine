using FearEngine;
using FearEngine.GameObjects;
using FearEngine.Lighting;
using FearEngine.Resources.Materials;
using FearEngine.Resources.Meshes;
using FearEngine.Scenes;
using FearEngine.Timer;
using SharpDX;

namespace FearEngineTests.FullScaleProjects.Games
{
    class IBLMaterialDemo: FearEngine.FearGame
    {
        Scene scene;
        GameObject cam;

        public void Startup(FearEngineImpl engine)
        {
            Light light = engine.LightFactory.CreateDirectionalLight();

            cam = engine.GameObjectFactory.CreateGameObject("Camera");
            scene = engine.SceneFactory.CreateSceneWithSingleLight(engine.CameraFactory.CreateDebugCamera(cam), light);

            Material material = engine.Resources.GetMaterial("IBLTest");

            int numOfSpheres = 10;
            float radiusOfSphere = 1.0f;
            float bufferBetweenSpheres = 0.2f;
            float distanceBetweenSphereCenters = (radiusOfSphere * 2.0f) + bufferBetweenSpheres;
            float sphereXCoord = -((float)numOfSpheres / 2.0f) * distanceBetweenSphereCenters;
            float sphereYCoord = radiusOfSphere + bufferBetweenSpheres;

            for (int i = 0; i < numOfSpheres; i++)
            {
                GameObject sphere = engine.GameObjectFactory.CreateGameObject("Sphere" + i);
                Mesh mesh = engine.Resources.GetMesh("Sphere");

                sphere.Transform.MoveTo(new Vector3(sphereXCoord, sphereYCoord, 0.0f));
                sphereXCoord += distanceBetweenSphereCenters;

                SceneObject sphereSceneObject = new SceneObject(sphere, mesh, material);
                scene.AddSceneObject(sphereSceneObject);
            }

            scene.EnableShadows();
        }

        public void Update(GameTimer gameTime)
        {
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
