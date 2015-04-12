using FearEngine.Cameras;
using FearEngine.DeviceState;
using FearEngine.DeviceState.SamplerStates;
using FearEngine.GameObjects;
using FearEngine.Lighting;
using FearEngine.Resources;
using FearEngine.Resources.Managment;
using FearEngine.Resources.Materials;
using FearEngine.Shadows;
using Ninject.Modules;
using SharpDX;
using Ninject;
using FearEngine.Inputs;
using SharpDX.Toolkit.Input;
using FearEngine.Scenes;
using FearEngine.Resources.Meshes;

namespace FearEngine
{
    public class FearEngineNinjectModule : NinjectModule
    {
        SharpDXGraphicsDevice existingDevice;
        FearResourceManager resMan;
        MouseManager mouseManager;
        KeyboardManager keyManager;

        public FearEngineNinjectModule(
            SharpDX.Toolkit.Graphics.GraphicsDevice device,
            FearResourceManager resman,
            MouseManager mouseMan,
            KeyboardManager keyMan)
        {
            existingDevice = new SharpDXGraphicsDevice(device);
            resMan = resman;

            mouseManager = mouseMan;
            keyManager = keyMan;
        }

        public override void Load()
        {
            Bind<FearGraphicsDevice>().ToConstant<SharpDXGraphicsDevice>(existingDevice);

            Bind<RasteriserState>().To<DefaultRasteriserState>().InSingletonScope().Named("Default");
            Bind<RasteriserState>().To<ShadowBiasedDepthRasteriserState>().InSingletonScope().Named("ShadowBiasedDepth");

            Bind<SamplerState>().To<ShadowMapComparisonSampler>().InSingletonScope().Named("ShadowMapComparison");

            Bind<Material>().To<FearMaterial>().InSingletonScope().Named("DepthWrite")
                .WithConstructorArgument("n", "DepthWrite")
                .WithConstructorArgument("resMan", resMan);

            Bind<ShadowTechnique>().To<BasicShadowTechnique>().Named("BasicShadowTechnique");

            Bind<Light>().To<DirectionalLight>();

            Bind<Input>().To<FearInput>()
                .InSingletonScope()
                .WithConstructorArgument("m", mouseManager)
                .WithConstructorArgument("keyb", keyManager);            

            Bind<GameObject>().To<BaseGameObject>().Named("FirstPersonCameraObject").WithConstructorArgument("name", "FirstPersonCamera");
            Bind<Updateable>().To<CameraControllerComponent>().Named("FirstPersonMovementComponent");

            Bind<Camera>().To<FearCamera>().InSingletonScope();

            Bind<MeshRenderer>().To<BasicMeshRenderer>().InSingletonScope();

            Bind<Scene>().To<BasicScene>()
                .WithConstructorArgument("meshRend", Kernel.Get<MeshRenderer>())
                .WithConstructorArgument("cam", Kernel.Get<Camera>())
                .WithConstructorArgument("light", Kernel.Get<Light>())
                .WithConstructorArgument("shadTech", Kernel.Get<ShadowTechnique>());
        }
    }
}
