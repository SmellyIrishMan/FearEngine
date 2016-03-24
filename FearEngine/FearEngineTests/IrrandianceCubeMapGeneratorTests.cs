using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine;
using FearEngine.Resources.Loaders;
using FearEngine.Resources.ResourceFiles.ResourceFileInformation;
using FearEngine.Resources;
using SharpDX.Direct3D11;
using FearEngine.Resources.Materials;

namespace FearEngineTests
{
    [TestClass]
    public class IrrandianceCubeMapGeneratorTests
    {
        [TestMethod]
        public void GenerateIrradianceCubemapFromCubemap()
        {
            //Given
            FearGraphicsDevice device = new SharpDXGraphicsDevice(SharpDX.Toolkit.Graphics.GraphicsDevice.New(DeviceCreationFlags.Debug));
            Material computeShader = LoadComputeShader(device);

            IrradianceCubeMapGenerator irrCubeGen = new IrradianceCubeMapGenerator(device, computeShader);

            TextureCube source = LoadOriginalCubemap(device);
            //When
            irrCubeGen.GenerateIrradianceCubemapFromTextureCube(source);
        }

        private static TextureCube LoadOriginalCubemap(FearGraphicsDevice device)
        {
            TextureLoader loader = new TextureLoader(device);

            ResourceInformation cubeInfo = new TextureResourceInformation();
            cubeInfo.UpdateInformation("Filepath", "C:\\Users\\Andy\\Documents\\Coding\\Visual Studio 2012\\Projects\\FearEngine\\Resources\\Textures\\Cubemaps\\LancellottiChapel\\LancellottiChapelCube.dds");
            cubeInfo.UpdateInformation("IsCubemap", "true");

            return (TextureCube)loader.Load(cubeInfo);
        }

        private Material LoadComputeShader(FearGraphicsDevice device)
        {
            MaterialLoader loader = new MaterialLoader(device);
            ResourceInformation info = new MaterialResourceInformation();
            info.UpdateInformation("Filepath", "C:\\Users\\Andy\\Documents\\Coding\\Visual Studio 2012\\Projects\\FearEngine\\Resources\\Shaders\\ComputeIrradiance.fx");
            info.UpdateInformation("Technique", "ComputeIrradianceMips");

            return (Material)loader.Load(info);
        }
    }
}
