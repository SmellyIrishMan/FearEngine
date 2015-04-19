using FearEngine;
using FearEngine.GameObjects;
using FearEngine.Resources.Meshes;
using FearEngine.Scenes;
using FearEngine.Resources.Materials;
using FearEngine.Timer;
using SharpDX.Direct3D11;
using FearEngine.Resources.Loaders;
using FearEngine.Resources.ResourceFiles.ResourceFileInformation;
using FearEngine.Resources;

namespace FearEngineExeTest
{
    class SimpleMainEntry
    {
        static int Main(string[] args)
        {
            FearGameFactory appFactory = new FearGameFactory();
            FearGame game = new TestGame();
            appFactory.CreateAndRunFearGame(game);

            return 0;
        }
    }

    class TestGame : FearEngine.FearGame
    {
        Scene scene;
        GameObject cam;
        SharpDX.Toolkit.Graphics.GraphicsDevice device;

        IrradianceCubeMapGenerator irrCubeGen;
        TextureCube source;

        public void Startup(FearEngineImpl engine)
        {
            cam = engine.GameObjectFactory.CreateGameObject("Camera");
            device = engine.Device;

            scene = engine.SceneFactory.CreateSceneWithSingleLight(
                engine.CameraFactory.CreateDebugCamera(cam),
                engine.LightFactory.CreateDirectionalLight());

            GameObject cube = new BaseGameObject("Cube");
            Mesh mesh = engine.Resources.GetMesh("BOX");
            Material material = engine.Resources.GetMaterial("NormalLit");

            SceneObject litCube = new SceneObject(cube, mesh, material);

            scene.AddSceneObject(litCube);

            FearGraphicsDevice dev2 = new SharpDXGraphicsDevice(engine.Device);
            Material computeShader = LoadComputeShader(dev2);

            irrCubeGen = new IrradianceCubeMapGenerator(dev2, computeShader);
            source = LoadOriginalCubemap(dev2);
        }

        public void Update(GameTimer gameTime)
        {
            cam.Update(gameTime);
        }

        public void Draw(GameTimer gameTime)
        {
            scene.Render(gameTime);

            irrCubeGen.GenerateIrradianceCubemapFromTextureCube(source);
        }

        private static TextureCube LoadOriginalCubemap(FearGraphicsDevice device)
        {
            TextureLoader loader = new TextureLoader(device);

            ResourceInformation cubeInfo = new TextureResourceInformation();
            cubeInfo.UpdateInformation("Filepath", "C:\\Users\\Andy\\Documents\\Coding\\Visual Studio 2012\\Projects\\FearEngine\\Resources\\Textures\\Cubemaps\\LancellottiChapel\\LancellottiChapelCube.dds");
            cubeInfo.UpdateInformation("IsCubemap", "true");

            return (TextureCube)loader.Load(cubeInfo);
        }

        private Material LoadComputeShader(FearGraphicsDevice device)
        {
            MaterialLoader loader = new MaterialLoader(device);
            ResourceInformation info = new MaterialResourceInformation();
            info.UpdateInformation("Filepath", "C:\\Users\\Andy\\Documents\\Coding\\Visual Studio 2012\\Projects\\FearEngine\\Resources\\Shaders\\ComputeIrradiance.fx");
            info.UpdateInformation("Technique", "ComputeIrradianceMips");

            return (Material)loader.Load(info);
        }

        public void Shutdown()
        {
        }
    }
}