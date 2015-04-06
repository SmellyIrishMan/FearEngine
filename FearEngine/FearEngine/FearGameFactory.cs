using FearEngine.Input;
using FearEngine.Resources.Loaders;
using FearEngine.Resources.Managment;
using FearEngine.Resources.Managment.Loaders;
using FearEngine.Resources.Managment.Loaders.Collada;
using FearEngine.Resources.Meshes;
using FearEngine.Scene;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Input;
using System.IO;
using Ninject;
using Ninject.Parameters;

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

            dependencyKernel = new StandardKernel(new FearEngineNinjectModule(device));

            ResourceDirectory resDir = CreateResourceDirectory();
            MeshLoader meshLoader = new MeshLoader(device, new ColladaMeshLoader(), new VertexBufferFactory());
            FearResourceManager resMan = new FearResourceManager(resDir, new MaterialLoader(device), meshLoader, new TextureLoader(device));

            FearEngine.Techniques.TechniqueFactory techFac = new FearEngine.Techniques.TechniqueFactory(engine.GetDevice(), resMan);
            FearEngine.Lighting.LightFactory lightFac = new FearEngine.Lighting.LightFactory();
            MeshRendererFactory meshRendFac = new MeshRendererFactory();
            SceneFactory sceneFactory = new SceneFactory(meshRendFac.CreateMeshRenderer(device), techFac, lightFac);

            engine.InjectDependencies(resMan, 
                new FearInput(new MouseManager(engine), new KeyboardManager(engine)),
                sceneFactory);
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