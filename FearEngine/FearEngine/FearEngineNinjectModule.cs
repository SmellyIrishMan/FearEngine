using FearEngine.Cameras;
using FearEngine.DeviceState;
using FearEngine.DeviceState.SamplerStates;
using FearEngine.GameObjects;
using FearEngine.Lighting;
using FearEngine.Resources.Loaders;
using FearEngine.Shadows;
using Ninject.Modules;
using Ninject.Extensions.Factory;
using FearEngine.Inputs;
using SharpDX.Toolkit.Input;
using FearEngine.Scenes;
using FearEngine.Resources.Meshes;
using FearEngine.Resources.Management;
using FearEngine.Resources.Loaders.Loaders.Collada;
using System.IO;
using FearEngine.GameObjects.Updateables;

namespace FearEngine
{
    public class FearEngineNinjectModule : NinjectModule
    {
        SharpDXGraphicsDevice existingDevice;
        MouseManager mouseManager;
        KeyboardManager keyManager;

        public FearEngineNinjectModule(
            SharpDX.Toolkit.Graphics.GraphicsDevice device,
            MouseManager mouseMan,
            KeyboardManager keyMan)
        {
            existingDevice = new SharpDXGraphicsDevice(device);

            mouseManager = mouseMan;
            keyManager = keyMan;
        }

        public override void Load()
        {
            Bind<FearGraphicsDevice>().ToConstant<SharpDXGraphicsDevice>(existingDevice);
            Bind<Input>().To<FearInput>()
                .InSingletonScope()
                .WithConstructorArgument("m", mouseManager)
                .WithConstructorArgument("keyb", keyManager);

            Bind<ResourceLoader>().To<MeshLoader>()
                .InSingletonScope()
                .Named("MeshLoader")
                .WithConstructorArgument("formatLoader", new ColladaMeshLoader())
                .WithConstructorArgument("vertBufferFactory", new VertexBufferFactory());

            Bind<ResourceLoader>().To<TextureLoader>().InSingletonScope().Named("TextureLoader");
            Bind<ResourceLoader>().To<MaterialLoader>().InSingletonScope().Named("MaterialLoader");
            
            Bind<ResourceDirectory>().To<ResourceDirectory>()
                .InSingletonScope()
                .WithConstructorArgument("rootResourcePath", GetResourceDirectoryPath())
                .WithConstructorArgument("fileFactory", new ResourceFileFactory());
            Bind<FearResourceManager>().To<FearResourceManager>().InSingletonScope();

            Bind<RasteriserState>().To<DefaultRasteriserState>().InSingletonScope().Named("Default");
            Bind<RasteriserState>().To<ShadowBiasedDepthRasteriserState>().InSingletonScope().Named("ShadowBiasedDepth");

            Bind<SamplerState>().To<ShadowMapComparisonSampler>().InSingletonScope().Named("ShadowMapComparison");

            Bind<ShadowTechnique>().To<BasicShadowTechnique>().Named("BasicShadowTechnique");

            Bind<GameObjectFactory>().ToFactory();
            Bind<UpdateableFactory>().ToFactory();
            Bind<SceneFactory>().ToFactory();
            Bind<LightFactory>().ToFactory();
            Bind<CameraFactory>().To<CameraFactory>().InSingletonScope();

            Bind<MeshRenderer>().To<BasicMeshRenderer>().InSingletonScope();
        }

        private string GetResourceDirectoryPath()
        {
            DirectoryInfo resourceDir = new System.IO.DirectoryInfo(System.Environment.CurrentDirectory);
            resourceDir = resourceDir.Parent.Parent;
            string resPath = System.IO.Path.Combine(resourceDir.FullName, "Resources");
            ResourceDirectory resDir = new ResourceDirectory(resPath, new ResourceFileFactory());
            return resPath;
        }
    }
}
