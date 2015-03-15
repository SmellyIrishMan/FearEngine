using FearEngine.Input;
using FearEngine.Resources.Loaders;
using FearEngine.Resources.Managment;
using FearEngine.Resources.Managment.Loaders;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Input;
using System.IO;

namespace FearEngine
{
    public class FearGameFactory
    {
        public FearEngineImpl CreateAndRunFearGame(FearGame game)
        {
            FearEngineImpl engine = new FearEngineImpl(game);

            GraphicsDeviceManager graphicsMan = new GraphicsDeviceManager(engine);
            engine.SetupDeviceManager(graphicsMan);

            engine.Initialised += new EngineInitialisedHandler(OnEngineInitialised);
            engine.Run();

            return engine;
        }

        public void OnEngineInitialised(FearEngineImpl engine)
        {
            engine.Initialised -= new EngineInitialisedHandler(OnEngineInitialised);

            GraphicsDevice device = engine.GetDevice();

            ResourceDirectory resDir = CreateResourceDirectory();
            FearResourceManager resMan = new FearResourceManager(resDir, new MaterialLoader(device), new MeshLoader(device), new TextureLoader(device));

            engine.InjectDependencies(resMan, new FearInput(new MouseManager(engine), new KeyboardManager(engine)));
        }

        private static ResourceDirectory CreateResourceDirectory()
        {
            DirectoryInfo resourceDir = new System.IO.DirectoryInfo(System.Environment.CurrentDirectory);
            resourceDir = resourceDir.Parent.Parent;
            string resPath = System.IO.Path.Combine(resourceDir.FullName, "Resources");
            ResourceDirectory resDir = new ResourceDirectory(resPath, new ResourceFileFactory());
            return resDir;
        }
    }
}