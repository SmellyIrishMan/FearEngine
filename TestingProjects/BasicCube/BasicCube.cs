using FearEngine.Resources.Meshes;
using System;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using FearEngine.Resources.Managment;

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
            BasicCube game = new BasicCube();
            game.Run();
        }
        Mesh cube;
        MeshRenderer meshRenderer;
        FearEngine.Resources.Material material;

        private VertexInputLayout inputLayout;

        protected override void Initialize()
        {
            base.Initialize();

            meshRenderer = new MeshRenderer();
        }

        protected override void LoadContent()
        {
            cube = ResourceManager.GetMesh("DEFAULT_MESH");
            material = ResourceManager.GetMaterial("NormalLit");

            inputLayout = VertexInputLayout.FromBuffer(0, cube.GetVertexBuffer());

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            //InputManager.Update();
            //MainCamera.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GetDevice().Clear(new SharpDX.Color4(0.2f, 0.0f, 0.2f, 1.0f));

            meshRenderer.RenderMesh(cube, material);

            base.Draw(gameTime);
        }
    }
}
