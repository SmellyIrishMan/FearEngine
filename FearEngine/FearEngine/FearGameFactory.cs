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

            GraphicsDevice dev = graphicsMan.GraphicsDevice;

            DirectoryInfo resourceDir = new System.IO.DirectoryInfo(System.Environment.CurrentDirectory);
            resourceDir = resourceDir.Parent.Parent;
            string resPath = System.IO.Path.Combine(resourceDir.FullName, "Resources");
            ResourceDirectory resDir = new ResourceDirectory(resPath, new ResourceFileFactory());
            FearResourceManager resMan = new FearResourceManager(resDir, new MaterialLoader(dev), new MeshLoader(dev), new TextureLoader(dev));

            engine.SetupApp(graphicsMan, resMan, new FearInput(new MouseManager(engine), new KeyboardManager(engine)));

            engine.Run();

            return engine;
        }
    }
}