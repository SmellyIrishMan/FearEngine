using FearEngine.Inputs;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Input;
using Ninject;
using FearEngine.Resources.Management;
using FearEngine.GameObjects;
using FearEngine.GameObjects.Updateables;
using FearEngine.Scenes;
using FearEngine.Cameras;
using FearEngine.Lighting;

namespace FearEngine
{
    public class FearGameFactory
    {

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

            StandardKernel kernel = new StandardKernel(new FearEngineNinjectModule(engine.Device,
                new MouseManager(engine),
                new KeyboardManager(engine)));

            engine.InjectDependencies(kernel.Get<FearResourceManager>(),
                kernel.Get<Input>(),
                kernel.Get<GameObjectFactory>(),
                kernel.Get<UpdateableFactory>(),
                kernel.Get<SceneFactory>(),
                kernel.Get<LightFactory>(),
                kernel.Get<CameraFactory>());
        }
    }
}