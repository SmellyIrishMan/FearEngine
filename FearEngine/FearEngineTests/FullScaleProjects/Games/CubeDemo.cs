using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine;
using FearEngine.Resources.Meshes;
using SharpDX.Toolkit;
using FearEngine.Resources;
using FearEngine.Scene;
using FearEngine.GameObjects;

namespace FearEngineTests.FullScaleProjects.Games
{
    public class CubeDemo : FearEngine.FearGame
    {
        Scene scene;

        public void Startup(FearEngineImpl engine)
        {
            scene = engine.CreateEmptyScene();

            GameObject cube = new GameObject("Cube");
            Mesh mesh = engine.GetResourceManager().GetMesh("BOX");
            Material material = engine.GetResourceManager().GetMaterial("NormalLit");

            SceneObject litCube = new SceneObject(cube, mesh, material);

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
