using FearEngine;
using FearEngine.Resources.Meshes;

namespace FearEngineTests.FullScaleProjects.Games
{
    public class TeapotDemo : FearEngine.FearGame
    {
        FearEngineImpl fearEngine;

        RenderableMesh cube;
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
            meshRenderer.RenderMesh(fearEngine.GetDevice(), cube, material, fearEngine.GetMainCamera());
        }

        public void Shutdown()
        {
        }
    }
}