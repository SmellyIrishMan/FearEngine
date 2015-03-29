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
        FearEngine.Resources.Material drawNormalsMaterial;

        public void Startup(FearEngineImpl engine)
        {
            fearEngine = engine;

            meshRenderer = new MeshRenderer();

            mesh = fearEngine.GetResourceManager().GetMesh("PLANE");
            
            //drawNormalsMaterial = fearEngine.GetResourceManager().GetMaterial("DrawNormals");
            material = fearEngine.GetResourceManager().GetMaterial("NormalMapped");

            material.SetParameterResource("gAlbedo", fearEngine.GetResourceManager().GetTexture("GravelCobble"));
            material.SetParameterResource("gNormal", fearEngine.GetResourceManager().GetTexture("GravelCobbleNormal"));            
        }

        public void Update(FearGameTime gameTime)
        {

        }

        public void Draw(FearGameTime gameTime)
        {
            meshRenderer.RenderMesh(fearEngine.GetDevice(), mesh, material, fearEngine.GetMainCamera());
            //meshRenderer.RenderMesh(fearEngine.GetDevice(), mesh, drawNormalsMaterial, fearEngine.GetMainCamera());
        }

        public void Shutdown()
        {
        }
    }
}
