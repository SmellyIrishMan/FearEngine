using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine.Resources.Loaders;
using FearEngine.Resources;
using FearEngine.Resources.Materials;
using FearEngine.Resources.ResourceFiles.ResourceFileInformation;
using FearEngine;

namespace FearEngineTests
{
    [TestClass]
    public class ResourceLoadingTests
    {
        [TestMethod]
        public void LoadTexture()
        {
            //Given
            SharpDX.Toolkit.Graphics.GraphicsDevice device = SharpDX.Toolkit.Graphics.GraphicsDevice.New();
            TextureLoader loader = new TextureLoader(new SharpDXGraphicsDevice(device));
            ResourceInformation info = new TextureResourceInformation();
            info.UpdateInformation("Filepath", "C:\\Users\\Andy\\Documents\\Coding\\Visual Studio 2012\\Projects\\FearEngine\\Resources\\Textures\\DefaultTexture.png");

            //When
            Texture texture = (Texture)loader.Load(info);

            //Then
            Assert.IsTrue(texture.Width == 1024);
            Assert.IsTrue(texture.Height == 1024);

            device.Dispose();
        }

        [TestMethod]
        public void LoadSRGBTexture()
        {
            //Given
            SharpDX.Toolkit.Graphics.GraphicsDevice device = SharpDX.Toolkit.Graphics.GraphicsDevice.New();
            TextureLoader loader = new TextureLoader(new SharpDXGraphicsDevice(device));
            ResourceInformation info = new TextureResourceInformation();
            info.UpdateInformation("Filepath", "C:\\Users\\Andy\\Documents\\Coding\\Visual Studio 2012\\Projects\\FearEngine\\Resources\\Textures\\GammaGradient.png");
            info.UpdateInformation("IsLinear", "false");

            //When
            Texture texture = (Texture)loader.Load(info);

            //Then
            Assert.IsTrue(texture.Width == 512);
            Assert.IsTrue(texture.Height == 512);

            device.Dispose();
        }

        [TestMethod]
        public void LoadMaterial()
        {
            //Given
            SharpDX.Toolkit.Graphics.GraphicsDevice device = SharpDX.Toolkit.Graphics.GraphicsDevice.New();
            MaterialLoader loader = new MaterialLoader(new SharpDXGraphicsDevice(device));
            ResourceInformation info = new MaterialResourceInformation();
            info.UpdateInformation("Filepath", "C:\\Users\\Andy\\Documents\\Coding\\Visual Studio 2012\\Projects\\FearEngine\\Resources\\Shaders\\Textured.fx");
            info.UpdateInformation("Technique", "TexturedNoLighting");

            //When
            Material material = (Material)loader.Load(info);

            //Then
            Assert.IsTrue(material.IsLoaded());

            device.Dispose();
        }
    }
}
