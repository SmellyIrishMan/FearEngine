using FearEngine.DeviceState;
using FearEngine.DeviceState.SamplerStates;
using Ninject.Modules;

namespace FearEngine
{
    public class FearEngineNinjectModule : NinjectModule
    {
        SharpDXGraphicsDevice existingDevice;

        public FearEngineNinjectModule(SharpDX.Toolkit.Graphics.GraphicsDevice device)
        {
            existingDevice = new SharpDXGraphicsDevice(device);
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
        }
    }
}
