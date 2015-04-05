﻿using FearEngine;
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

            scene = new Scene(engine.GetDevice(), fearEngine.GetResourceManager().GetMaterial("DepthWrite"), new MeshRenderer(engine.GetDevice()), fearEngine.GetMainCamera());

            GameObject teapot = new GameObject("Teapot");
            Mesh mesh = fearEngine.GetResourceManager().GetMesh("TEAPOT");
            Material material = fearEngine.GetResourceManager().GetMaterial("ShadowTesting");
            SceneObject shadowedTeapot = new SceneObject(teapot, mesh, material);
            scene.AddSceneObject(shadowedTeapot);

            GameObject planeObj = new GameObject("Plane");
            planeObj.Transform.SetScale(3.0f);
            Mesh plane = fearEngine.GetResourceManager().GetMesh("PLANE");
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
