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
    class ShaderModelComparisonDemo : FearEngine.FearGame
    {
        GameObject teapotPhong;
        GameObject teapotPBR;
        Scene scene;

        public void Startup(FearEngineImpl engine)
        {
            FearEngine.DeviceState.DeviceStateFactory devStateFac = new FearEngine.DeviceState.DeviceStateFactory(engine.GetDevice());
            FearEngine.Techniques.TechniqueFactory techFac = new FearEngine.Techniques.TechniqueFactory(engine.GetDevice(), engine.GetResourceManager(), devStateFac);

            scene = new Scene(new MeshRenderer(engine.GetDevice()), engine.GetMainCamera(),
                new FearEngine.Lighting.LightFactory(),
                techFac,
                devStateFac);

            float seperation = 6.0f;

            teapotPhong = new GameObject("TeapotPhong");
            teapotPhong.Transform.MoveTo(teapotPhong.Transform.Position + (teapotPhong.Transform.Right * seperation));
            teapotPhong.AddUpdatable(new ContinuousRotationAroundY());

            teapotPBR = new GameObject("TeapotPBR");
            teapotPBR.Transform.MoveTo(teapotPhong.Transform.Position + (-teapotPhong.Transform.Right * seperation));
            teapotPBR.AddUpdatable(new ContinuousRotationAroundY());

            Mesh mesh = engine.GetResourceManager().GetMesh("TEAPOT");

            Material phong = engine.GetResourceManager().GetMaterial("NormalLit");
            Material pbr = engine.GetResourceManager().GetMaterial("PBR_GGX");

            SceneObject phongTeapot = new SceneObject(teapotPhong, mesh, phong);
            SceneObject pbrTeapot = new SceneObject(teapotPBR, mesh, pbr);

            scene.AddSceneObject(phongTeapot);
            scene.AddSceneObject(pbrTeapot);
        }

        public void Update(GameTime gameTime)
        {
            teapotPhong.Update(gameTime);
            teapotPBR.Update(gameTime);
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
