using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine;
using FearEngine.Resources.Meshes;

namespace FearEngineTests.FullScaleProjects.Games
{
    public class NormalMappedMeshDemo : FearEngine.FearGame
    {
        FearEngineImpl fearEngine;

        RenderableMesh mesh;
        MeshRenderer meshRenderer;
        FearEngine.Resources.Material material;

        public void Startup(FearEngineImpl engine)
        {
            fearEngine = engine;

            meshRenderer = new MeshRenderer();

            mesh = fearEngine.GetResourceManager().GetMesh("DIFFERENTCUBE");
            material = fearEngine.GetResourceManager().GetMaterial("Textured");
            material.SetParameterResource("gAlbedo", fearEngine.GetResourceManager().GetTexture("GammaGradient"));
        }

        public void Update(FearGameTime gameTime)
        {

        }

        public void Draw(FearGameTime gameTime)
        {
            meshRenderer.RenderMesh(fearEngine.GetDevice(), mesh, material, fearEngine.GetMainCamera());
        }

        public void Shutdown()
        {
        }
    }
}
