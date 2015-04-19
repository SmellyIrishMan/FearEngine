using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDX.Direct3D11;
using SharpDX.Direct3D;
using FearEngine.Resources.Loaders;
using FearEngine.Resources.ResourceFiles.ResourceFileInformation;
using FearEngine;
using FearEngine.Resources.Materials;
using SharpDX;

namespace FearEngineTests
{
    [TestClass]
    public class MipGenerationTests
    {
        int numOfMips = 3;

        [TestMethod]
        public void MipGenerationSimple2LevelsOn2DTexture()
        {
            //Given
            SharpDX.Toolkit.Graphics.GraphicsDevice device = SharpDX.Toolkit.Graphics.GraphicsDevice.New(DeviceCreationFlags.Debug); 
            Material computeShader = LoadComputeShader(device);

            Texture2DDescription emptyTextureDesc = CreateTextureDescription();
            SharpDX.Direct3D11.Texture2D emptyTexture = new SharpDX.Direct3D11.Texture2D(device, emptyTextureDesc);

            UnorderedAccessViewDescription uavDesc = CreateUAVDescription(0);
            UnorderedAccessView uavMip0 = new UnorderedAccessView(device, emptyTexture, uavDesc);

            uavDesc = CreateUAVDescription(1);
            UnorderedAccessView uavMip1 = new UnorderedAccessView(device, emptyTexture, uavDesc);

            uavDesc = CreateUAVDescription(2);
            UnorderedAccessView uavMip2 = new UnorderedAccessView(device, emptyTexture, uavDesc);

            //When
            Vector4 mip0Colour = new Vector4(0.1f, 0.2f, 0.3f, 1.0f);
            Vector4 mip1Colour = new Vector4(0.0f, 0.5f, 0.0f, 1.0f);
            Vector4 mip2Colour = new Vector4(0.7f, 0.4f, 0.15f, 1.0f);

            computeShader.SetParameterResource("gOutput", uavMip0);
            computeShader.SetParameterValue("fillColour", mip0Colour);
            computeShader.Apply();
            device.Dispatch(1, 64, 1);

            computeShader.SetParameterResource("gOutput", uavMip1);
            computeShader.SetParameterValue("fillColour", mip1Colour);
            computeShader.Apply();
            device.Dispatch(1, 32, 1);

            computeShader.SetParameterResource("gOutput", uavMip2);
            computeShader.SetParameterValue("fillColour", mip2Colour);
            computeShader.Apply();
            device.Dispatch(1, 16, 1);

            Texture2DDescription bufferDesc = CreateLocalBufferDesc();
            SharpDX.Direct3D11.Texture2D localbuffer = new SharpDX.Direct3D11.Texture2D(device, bufferDesc);

            device.Copy(emptyTexture, localbuffer);

            DataStream data = new DataStream(8 * 64 * 64, true, true);
            DataBox box = ((DeviceContext)device).MapSubresource(localbuffer, 0, 0, MapMode.Read, MapFlags.None, out data);
            Half4 quickTest = data.ReadHalf4();
            ((DeviceContext)device).UnmapSubresource(localbuffer, 0);

            Vector4 conversion = (Vector4)quickTest;
            float distance = (conversion - mip0Colour).Length();
            Assert.IsTrue(distance < 0.0005f);
        }

        private static Material LoadComputeShader(SharpDX.Toolkit.Graphics.GraphicsDevice device)
        {
            MaterialLoader loader = new MaterialLoader(new SharpDXGraphicsDevice(device));
            ResourceInformation info = new MaterialResourceInformation();
            info.UpdateInformation("Filepath", "C:\\Users\\Andy\\Documents\\Coding\\Visual Studio 2012\\Projects\\FearEngine\\Resources\\Shaders\\ComputeTest.fx");
            info.UpdateInformation("Technique", "FillTexture");

            return (Material)loader.Load(info);
        }

        private Texture2DDescription CreateTextureDescription()
        {
            Texture2DDescription desc = new Texture2DDescription();
            desc.Width = 64;
            desc.Height = 64;
            desc.MipLevels = numOfMips;
            desc.ArraySize = 1;
            desc.Format = SharpDX.DXGI.Format.R16G16B16A16_Float;
            desc.SampleDescription.Count = 1;
            desc.SampleDescription.Quality = 0;
            desc.Usage = ResourceUsage.Default;
            desc.BindFlags = BindFlags.UnorderedAccess | BindFlags.ShaderResource;
            desc.CpuAccessFlags = CpuAccessFlags.None;
            desc.OptionFlags = ResourceOptionFlags.None;

            return desc;
        }

        private ShaderResourceViewDescription CreateSRVDescription()
        {
            ShaderResourceViewDescription desc = new ShaderResourceViewDescription();
            desc.Format = SharpDX.DXGI.Format.R16G16B16A16_Float;
            desc.Dimension = ShaderResourceViewDimension.Texture2D;
            desc.Texture2D.MipLevels = numOfMips;
            desc.Texture2D.MostDetailedMip = 0;
            return desc;
        }

        private UnorderedAccessViewDescription CreateUAVDescription(int mipSlice)
        {
            UnorderedAccessViewDescription desc = new UnorderedAccessViewDescription();
            desc.Format = SharpDX.DXGI.Format.R16G16B16A16_Float;
            desc.Dimension = UnorderedAccessViewDimension.Texture2D;
            desc.Texture2D.MipSlice = mipSlice;
            return desc;
        }

        private Texture2DDescription CreateLocalBufferDesc()
        {
            Texture2DDescription desc = new Texture2DDescription();
            desc.Width = 64;
            desc.Height = 64;
            desc.MipLevels = numOfMips;
            desc.ArraySize = 1;
            desc.Format = SharpDX.DXGI.Format.R16G16B16A16_Float;
            desc.SampleDescription.Count = 1;
            desc.SampleDescription.Quality = 0;
            desc.Usage = ResourceUsage.Staging;
            desc.BindFlags = BindFlags.None;
            desc.CpuAccessFlags = CpuAccessFlags.Read;
            desc.OptionFlags = ResourceOptionFlags.None;

            return desc;
        }
    }
}
