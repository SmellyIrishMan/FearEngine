﻿using FearEngine;
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
        GameObject cam;

        public void Startup(FearEngineImpl engine)
        {
            rotatingLight = engine.GameObjectFactory.CreateGameObject("RotatingLightFixture");
            rotatingLight.AddUpdatable(engine.UpdateableFactory.CreateContinuousRandomSlerp( rotatingLight.Transform, 0.25f));

            Light light = engine.LightFactory.CreateDirectionalLight();
            TransformAttacher attacher = (TransformAttacher)light;
            attacher.AttactToTransform(rotatingLight.Transform);

            cam = engine.GameObjectFactory.CreateGameObject("Camera");
            scene = engine.SceneFactory.CreateSceneWithSingleLight(engine.CameraFactory.CreateDebugCamera(cam), light);

            GameObject teapot = engine.GameObjectFactory.CreateGameObject("Teapot");
            Mesh mesh = engine.Resources.GetMesh("TEAPOT");
            Material material = engine.Resources.GetMaterial("ShadowedPBR");
            SceneObject shadowedTeapot = new SceneObject(teapot, mesh, material);
            scene.AddSceneObject(shadowedTeapot);

            GameObject planeObj = engine.GameObjectFactory.CreateGameObject("Plane");
            planeObj.Transform.SetScale(3.0f);
            Mesh plane = engine.Resources.GetMesh("PLANE");
            SceneObject shadowedPlane = new SceneObject(planeObj, plane, material);
            scene.AddSceneObject(shadowedPlane);

            scene.EnableShadows();
        }

        public void Update(GameTimer gameTime)
        {
            cam.Update(gameTime);
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
