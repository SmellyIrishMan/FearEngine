using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine;
using FearEngine.Resources.Meshes;

namespace FearEngineTests.FullScaleProjects.Games
{
    public class TextureCubeDemo : FearEngine.FearGame
    {
        FearEngineImpl fearEngine;

        Mesh cube;
        MeshRenderer meshRenderer;
        FearEngine.Resources.Material material;

        public void Startup(FearEngineImpl engine)
        {
            fearEngine = engine;

            meshRenderer = new MeshRenderer();

            cube = fearEngine.GetResourceManager().GetMesh("BOX");
            material = fearEngine.GetResourceManager().GetMaterial("Textured");
            material.SetParameterResource("gAlbedo", fearEngine.GetResourceManager().GetTexture("DEFAULT"));
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
