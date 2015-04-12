using FearEngine.Cameras;
using FearEngine.DeviceState;
using FearEngine.DeviceState.SamplerStates;
using FearEngine.GameObjects;
using FearEngine.Lighting;
using FearEngine.Resources;
using FearEngine.Resources.Loaders;
using FearEngine.Resources.Materials;
using FearEngine.Shadows;
using Ninject.Modules;
using SharpDX;
using Ninject;
using FearEngine.Inputs;
using SharpDX.Toolkit.Input;
using FearEngine.Scenes;
using FearEngine.Resources.Meshes;
using FearEngine.Resources.Management;
using FearEngine.Resources.Loaders.Loaders.Collada;
using System.IO;

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


            Bind<ResourceLoader>().To<TextureLoader>().InSingletonScope().Named("TextureLoader");
            
            Bind<ResourceLoader>().To<MeshLoader>()
                .InSingletonScope()
                .Named("MeshLoader")
                .WithConstructorArgument("l", new ColladaMeshLoader())
                .WithConstructorArgument("fac", new VertexBufferFactory());

            Bind<ResourceLoader>().To<MaterialLoader>().InSingletonScope().Named("MaterialLoader");

            DirectoryInfo resourceDir = new System.IO.DirectoryInfo(System.Environment.CurrentDirectory);
            resourceDir = resourceDir.Parent.Parent;
            string resPath = System.IO.Path.Combine(resourceDir.FullName, "Resources");
            ResourceDirectory resDir = new ResourceDirectory(resPath, new ResourceFileFactory());
            
            Bind<ResourceDirectory>().To<ResourceDirectory>()
                .InSingletonScope()
                .WithConstructorArgument("rootResourcePath", resPath)
                .WithConstructorArgument("fileFactory", new ResourceFileFactory());
            Bind<FearResourceManager>().To<FearResourceManager>().InSingletonScope();

            Bind<RasteriserState>().To<DefaultRasteriserState>().InSingletonScope().Named("Default");
            Bind<RasteriserState>().To<ShadowBiasedDepthRasteriserState>().InSingletonScope().Named("ShadowBiasedDepth");

            Bind<SamplerState>().To<ShadowMapComparisonSampler>().InSingletonScope().Named("ShadowMapComparison");

            Bind<ShadowTechnique>().To<BasicShadowTechnique>().Named("BasicShadowTechnique");

            Bind<Light>().To<DirectionalLight>();

            

            Bind<GameObject>().To<BaseGameObject>().Named("FirstPersonCameraObject").WithConstructorArgument("name", "FirstPersonCamera");
            Bind<Updateable>().To<CameraControllerComponent>().Named("FirstPersonMovementComponent");

            Bind<Camera>().To<FearCamera>().InSingletonScope();

            Bind<MeshRenderer>().To<BasicMeshRenderer>().InSingletonScope();

            Bind<Scene>().To<BasicScene>();
        }
    }
}
