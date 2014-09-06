﻿using FearEngine.Resources;
using FearEngine.Resources.Meshes;
using FearEngine.Logger;
using System;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;

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

        MeshReader meshReader;
        Mesh cube;
        MeshRenderer meshRenderer;
        FearEngine.Resources.Material material;

        protected override void Initialize()
        {
            base.Initialize();

            meshReader = new MeshReader();
            meshRenderer = new MeshRenderer();
            cube = meshReader.LoadMesh("C:\\Users\\Andy\\Documents\\Coding\\Visual Studio 2012\\Projects\\FearEngine\\Resources\\Models\\Box.DAE");

            material = ResourceManager.GetMaterial("TexturedLit");
        }

        protected override void Draw(GameTime gameTime)
        {
            GetDevice().Clear(new SharpDX.Color4(0.2f, 0.0f, 0.2f, 1.0f));

            meshRenderer.RenderMesh(cube, material);

            //InputManager.Update();
            //MainCamera.Update();
            base.Draw(gameTime);
        }

        //protected override void Shutdown()
        //{
        //    base.Shutdown();
        //}
    }
}
