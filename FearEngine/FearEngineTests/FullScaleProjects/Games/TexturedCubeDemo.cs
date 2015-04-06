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
            FearEngine.DeviceState.DeviceStateFactory devStateFac = new FearEngine.DeviceState.DeviceStateFactory(engine.GetDevice());
            FearEngine.Techniques.TechniqueFactory techFac = new FearEngine.Techniques.TechniqueFactory(engine.GetDevice(), engine.GetResourceManager(), devStateFac);

            scene = new Scene(new MeshRenderer(engine.GetDevice()), engine.GetMainCamera(),
                new FearEngine.Lighting.LightFactory(),
                techFac,
                devStateFac);

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
