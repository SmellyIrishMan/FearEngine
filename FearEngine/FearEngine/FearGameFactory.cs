using FearEngine.Inputs;
using FearEngine.Resources.Meshes;
using FearEngine.Scenes;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Input;
using System.IO;
using Ninject;
using Ninject.Parameters;
using FearEngine.Resources.Management;
using FearEngine.Resources.Loaders;
using FearEngine.Resources.Loaders.Loaders.Collada;

namespace FearEngine
{    
    public class FearGameFactory
    {
        public static IKernel dependencyKernel;

        public FearEngineImpl CreateAndRunFearGame(FearGame game)
        {
            FearEngineImpl engine = new FearEngineImpl(game, game.GetType().Name);

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

            dependencyKernel = new StandardKernel(new FearEngineNinjectModule(device,
                new MouseManager(engine),
                new KeyboardManager(engine)));

            engine.InjectDependencies(dependencyKernel.Get<FearResourceManager>(), dependencyKernel.Get<Input>());

            Input anotherTest = dependencyKernel.Get<Input>();
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