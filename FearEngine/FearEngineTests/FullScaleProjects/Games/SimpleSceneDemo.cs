using FearEngine;
using FearEngine.GameObjects;
using FearEngine.GameObjects.Updateables;
using FearEngine.Lighting;
using FearEngine.Resources;
using FearEngine.Resources.Meshes;
using FearEngine.Scene;
using SharpDX.Toolkit;
namespace FearEngineTests.FullScaleProjects.Games
{
    class SimpleSceneDemo : FearEngine.FearGame
    {
        GameObject teapot;
        Scene scene;

        public void Startup(FearEngineImpl engine)
        {
            FearEngine.DeviceState.DeviceStateFactory devStateFac = new FearEngine.DeviceState.DeviceStateFactory(engine.GetDevice());
            FearEngine.Techniques.TechniqueFactory techFac = new FearEngine.Techniques.TechniqueFactory(engine.GetDevice(), engine.GetResourceManager(), devStateFac);

            scene = new Scene(new MeshRenderer(engine.GetDevice()), engine.GetMainCamera(),
                new FearEngine.Lighting.LightFactory(),
                techFac,
                devStateFac);

            teapot = new GameObject("Teapot");
            teapot.AddUpdatable(new ContinuousRotationAroundY());

            Mesh mesh = engine.GetResourceManager().GetMesh("TEAPOT");
            Material material = engine.GetResourceManager().GetMaterial("NormalLit");

            SceneObject litTeapot = new SceneObject(teapot, mesh, material);

            scene.AddSceneObject(litTeapot);
        }

        public void Update(GameTime gameTime)
        {
            teapot.Update(gameTime);
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
