using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine;
using FearEngine.Resources.Meshes;
using SharpDX.Toolkit;
using FearEngine.GameObjects;
using FearEngine.Resources;
using FearEngine.Scene;

namespace FearEngineTests.FullScaleProjects.Games
{
    public class NormalMappedMeshDemo : FearEngine.FearGame
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

            GameObject teapot = new GameObject("FloorPlane");
            Mesh mesh = engine.GetResourceManager().GetMesh("PLANE");
            Material material = engine.GetResourceManager().GetMaterial("NormalMapped");
            material.SetParameterResource("gAlbedo", engine.GetResourceManager().GetTexture("GravelCobble"));
            material.SetParameterResource("gNormal", engine.GetResourceManager().GetTexture("GravelCobbleNormal"));

            //drawNormalsMaterial = fearEngine.GetResourceManager().GetMaterial("DrawNormals");

            SceneObject litCube = new SceneObject(teapot, mesh, material);

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
