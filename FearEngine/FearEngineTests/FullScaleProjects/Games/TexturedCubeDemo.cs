using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine;
using FearEngine.Resources.Meshes;
using SharpDX.Toolkit;
using FearEngine.GameObjects;
using FearEngine.Scene;
using FearEngine.Resources;

namespace FearEngineTests.FullScaleProjects.Games
{
    public class TextureCubeDemo : FearEngine.FearGame
    {
        Scene scene;

        public void Startup(FearEngineImpl engine)
        {
            scene = engine.CreateEmptyScene();

            GameObject cube = new GameObject("Cube");
            Mesh mesh = engine.GetResourceManager().GetMesh("BOX");
            Material material = engine.GetResourceManager().GetMaterial("Textured");
            material.SetParameterResource("gAlbedo", engine.GetResourceManager().GetTexture("GammaGradient"));

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
