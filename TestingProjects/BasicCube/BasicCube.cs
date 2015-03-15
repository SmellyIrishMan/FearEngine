using FearEngine.Resources.Meshes;
using System;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using FearEngine.Resources.Managment;
using FearEngine;

namespace BasicCube
{
    class BasicCube
    {
#if NETFX_CORE
        [MTAThread]
#else
        [STAThread]
#endif
        static void Main()
        {
            FearGameFactory appFactory = new FearGameFactory();
            appFactory.CreateAndRunFearGame(new BasicCubeGame());
        }
    }

    public class BasicCubeGame : FearEngine.FearGame
    {
        FearEngineImpl fearEngine;

        Mesh cube;
        MeshRenderer meshRenderer;
        FearEngine.Resources.Material material;

        public void Startup(FearEngineImpl engine)
        {
            fearEngine = engine;

            meshRenderer = new MeshRenderer();

            cube = fearEngine.GetResourceManager().GetMesh("TEAPOT");
            material = fearEngine.GetResourceManager().GetMaterial("NormalLit");
        }

        public void Update(FearGameTime gameTime)
        {

        }

        public void Draw(FearGameTime gameTime)
        {
            fearEngine.GetDevice().Clear(new SharpDX.Color4(0.2f, 0.0f, 0.2f, 1.0f));

            meshRenderer.RenderMesh(fearEngine.GetDevice(), cube, material, fearEngine.GetMainCamera());
        }

        public void Shutdown()
        {
        }
    }
}
