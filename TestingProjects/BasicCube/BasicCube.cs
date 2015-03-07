using FearEngine.Resources.Meshes;
using System;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using FearEngine.Resources.Managment;
using FearEngine;

namespace BasicCube
{
    class BasicCube : FearEngine.FearEngineApp
    {
#if NETFX_CORE
        [MTAThread]
#else
        [STAThread]
#endif
        static void Main()
        {
            BasicCube app = new BasicCube();

            FearAppFactory appFactory = new FearAppFactory();
            appFactory.CreateFearApp(app);

            app.Run();
        }
        MeshRenderable cube;
        MeshRenderer meshRenderer;
        FearEngine.Resources.Material material;

        protected override void Initialize()
        {
            base.Initialize();

            meshRenderer = new MeshRenderer();
        }

        protected override void LoadContent()
        {
            cube = resourceManager.GetMesh("TEAPOT");
            material = resourceManager.GetMaterial("NormalLit");

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GetDevice().Clear(new SharpDX.Color4(0.2f, 0.0f, 0.2f, 1.0f));

            meshRenderer.RenderMesh(GetDevice(), cube, material, mainCamera);

            base.Draw(gameTime);
        }
    }
}
