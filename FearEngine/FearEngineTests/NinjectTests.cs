using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using FearEngine.DeviceState;
using FearEngine;
using SharpDX.Toolkit.Graphics;
using SharpDX.Direct3D11;

namespace FearEngineTests
{
    [TestClass]
    public class NinjectTests
    {
        [TestMethod]
        public void CreateRasteriserStateWithNinject()
        {
            //Given
            IKernel kernal = new StandardKernel();

            kernal.Bind<SharpDXGraphicsDevice>().ToSelf().InSingletonScope();
            kernal.Bind<DefaultRasteriserState>().ToSelf().InSingletonScope();

            kernal.Bind<FearGraphicsDevice>().ToConstant<SharpDXGraphicsDevice>(new SharpDXGraphicsDevice(GraphicsDevice.New(DeviceCreationFlags.Debug)));
            kernal.Bind<RasteriserState>().To<DefaultRasteriserState>();

            //When
            RasteriserState state = kernal.Get<RasteriserState>();
            
            //Then
            Assert.IsTrue(state.GetType() == typeof(DefaultRasteriserState));
        }

        [TestMethod]
        public void CreateDifferentRasteriserStatesWithNinjectUsingNamedBindings()
        {
            //Given
            SharpDX.Toolkit.Graphics.GraphicsDevice device = SharpDX.Toolkit.Graphics.GraphicsDevice.New(DeviceCreationFlags.Debug);
            IKernel kernal = new StandardKernel();

            kernal.Bind<SharpDXGraphicsDevice>().ToSelf().InSingletonScope();
            kernal.Bind<DefaultRasteriserState>().ToSelf().InSingletonScope();
            kernal.Bind<ShadowBiasedDepthRasteriserState>().ToSelf().InSingletonScope();

            kernal.Bind<FearGraphicsDevice>().ToConstant<SharpDXGraphicsDevice>(new SharpDXGraphicsDevice(GraphicsDevice.New(DeviceCreationFlags.Debug)));
            kernal.Bind<RasteriserState>().To<DefaultRasteriserState>().Named("Default");
            kernal.Bind<RasteriserState>().To<ShadowBiasedDepthRasteriserState>().Named("ShadowBiasedDepth");

            //When
            RasteriserState state = kernal.Get<RasteriserState>("Default");

            //Then
            Assert.IsTrue(state.GetType() == typeof(DefaultRasteriserState));

            //When
            state = kernal.Get<RasteriserState>("ShadowBiasedDepth");

            //Then
            Assert.IsTrue(state.GetType() == typeof(ShadowBiasedDepthRasteriserState));
        }

        [TestMethod]
        public void CreateSingletonGraphicsDevice()
        {
            //Given
            IKernel dependencyKernel = new StandardKernel(new FearEngineNinjectModule(GraphicsDevice.New(DeviceCreationFlags.Debug), null, null));

            //When
            FearGraphicsDevice device = dependencyKernel.Get<FearGraphicsDevice>();

            //Then
            Assert.IsTrue(true);
        }
    }
}
