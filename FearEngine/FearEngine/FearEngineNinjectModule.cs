using FearEngine.DeviceState;
using FearEngine.DeviceState.SamplerStates;
using FearEngine.Resources;
using FearEngine.Resources.Managment;
using FearEngine.Resources.Materials;
using Ninject.Modules;

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

            Bind<DefaultRasteriserState>().ToSelf().InSingletonScope();
            Bind<RasteriserState>().To<DefaultRasteriserState>().Named("Default");
            Bind<ShadowBiasedDepthRasteriserState>().ToSelf().InSingletonScope();
            Bind<RasteriserState>().To<ShadowBiasedDepthRasteriserState>().Named("ShadowBiasedDepth");
            
            Bind<ShadowMapComparisonSampler>().ToSelf().InSingletonScope();
            Bind<SamplerState>().To<ShadowMapComparisonSampler>().Named("ShadowMapComparison");

            //Bind<FearMaterial>().ToSelf().WithConstructorArgument;
            Bind<Material>().To<FearMaterial>().Named("DepthWrite")
                .WithConstructorArgument("n", "DepthWrite")
                .WithConstructorArgument("resMan", resMan);
        }

        //For setting stuff up from within the module
        //Bind<IFoo>().To<Foo>().WithConstructorArgument("username",  context => context.Kernel.Get<IConfig>().Username)
    }
}
