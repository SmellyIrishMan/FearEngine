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

namespace FearEngine
{
    public class FearEngineNinjectModule : NinjectModule
    {
        SharpDXGraphicsDevice existingDevice;
        FearResourceManager resMan;

        public FearEngineNinjectModule(SharpDX.Toolkit.Graphics.GraphicsDevice device, FearResourceManager resman)
        {
            existingDevice = new SharpDXGraphicsDevice(device);
            resMan = resman;
        }

        public override void Load()
        {
            //Bind<SharpDXGraphicsDevice>().ToSelf().InSingletonScope();
            //Bind<FearGraphicsDevice>().To<SharpDXGraphicsDevice>();
            //Bind<FearGraphicsDevice>().To<SharpDXGraphicsDevice>().WithConstructorArgument("dev", SharpDX.Toolkit.Graphics.GraphicsDevice.New());
            Bind<FearGraphicsDevice>().ToConstant<SharpDXGraphicsDevice>(existingDevice);

            Bind<RasteriserState>().To<DefaultRasteriserState>().Named("Default");
            Bind<DefaultRasteriserState>().ToSelf().InSingletonScope();
            
            Bind<RasteriserState>().To<ShadowBiasedDepthRasteriserState>().Named("ShadowBiasedDepth");
            Bind<ShadowBiasedDepthRasteriserState>().ToSelf().InSingletonScope();
            
            Bind<SamplerState>().To<ShadowMapComparisonSampler>().Named("ShadowMapComparison");
            Bind<ShadowMapComparisonSampler>().ToSelf().InSingletonScope();

            Bind<Material>().To<FearMaterial>().Named("DepthWrite")
                .WithConstructorArgument("n", "DepthWrite")
                .WithConstructorArgument("resMan", resMan);
            Bind<FearMaterial>().ToSelf().InSingletonScope().Named("DepthWrite");

            Bind<ShadowTechnique>().To<BasicShadowTechnique>().Named("BasicShadowTechnique");

            Bind<Light>().To<DirectionalLight>();

            Bind<Camera>().To<FearCamera>();
            Bind<FearCamera>().ToSelf().InSingletonScope();

            Transform cameraTransform = new Transform();
            cameraTransform.MoveTo(new Vector3(1, 2, -5));
            Bind<GameObject>().To<BaseGameObject>().Named("FirstPersonCameraObject")
                .WithConstructorArgument("name", "MainCamera")
                .WithConstructorArgument("transform", cameraTransform);
        }

        //For setting stuff up from within the module
        //Bind<IFoo>().To<Foo>().WithConstructorArgument("username",  context => context.Kernel.Get<IConfig>().Username)
    }
}
