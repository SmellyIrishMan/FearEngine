using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDX.Direct3D11;
using SharpDX.Direct3D;
using FearEngine.Resources.Loaders;
using FearEngine.Resources.ResourceFiles.ResourceFileInformation;
using FearEngine;
using FearEngine.Resources.Materials;
using SharpDX;
using System.Collections.Generic;

namespace FearEngineTests
{
    [TestClass]
    public class MipGenerationTests
    {
        int numOfMips = 3;
        int textureSize = 64;

        [TestMethod]
        public void MipGenerationSimple2LevelsOn2DTexture()
        {
            //Given
            SharpDX.Toolkit.Graphics.GraphicsDevice device = SharpDX.Toolkit.Graphics.GraphicsDevice.New(DeviceCreationFlags.Debug); 
            Material computeShader = LoadComputeShader(device);
            List<Vector4> mipColours = new List<Vector4>( 
                new Vector4[]{ 
                    new Vector4(0.1f, 0.2f, 0.3f, 1.0f),
                    new Vector4(0.0f, 0.5f, 0.0f, 1.0f),
                    new Vector4(0.7f, 0.4f, 0.15f, 1.0f)});

            Texture2DDescription emptyTextureDesc = CreateTextureDescription();
            Texture2D emptyTexture = new Texture2D(device, emptyTextureDesc);

            List<UnorderedAccessView> mipViews = new List<UnorderedAccessView>();
            for (int mip = 0; mip < numOfMips; mip++)
            {
                UnorderedAccessViewDescription uavDesc = CreateUAVDescription(mip);
                mipViews.Add(new UnorderedAccessView(device, emptyTexture, uavDesc));
            }

            //When
            for (int mip = 0; mip < numOfMips; mip++)
            {
                FillMipSlice(device, computeShader, textureSize / (mip + 1), mipViews[mip], mipColours[mip]);
            }

            Texture2D readableTexture = CopyFilledTextureLocally(device, emptyTexture);

            for (int mip = 0; mip < numOfMips; mip++)
            {
                float variance = CheckVariationBetweenValueAndExpectedValue(device, textureSize / (mip + 1), mip, mipColours[mip], readableTexture);
                Assert.IsTrue(variance < 0.0005f);
            }
        }

        private static float CheckVariationBetweenValueAndExpectedValue(DeviceContext context, int textureSize, int mipSlice, Vector4 mip0Colour, Texture2D readableTexture)
        {
            DataStream data = new DataStream(8 * textureSize * textureSize, true, true);
            context.MapSubresource(readableTexture, mipSlice, 0, MapMode.Read, MapFlags.None, out data);
            Half4 quickTest = data.ReadHalf4();
            context.UnmapSubresource(readableTexture, mipSlice);

            Vector4 conversion = (Vector4)quickTest;
            float variance = (conversion - mip0Colour).Length();
            return variance;
        }

        private Texture2D CopyFilledTextureLocally(SharpDX.Toolkit.Graphics.GraphicsDevice device, SharpDX.Direct3D11.Texture2D emptyTexture)
        {
            Texture2DDescription bufferDesc = CreateLocalBufferDesc();
            SharpDX.Direct3D11.Texture2D localbuffer = new SharpDX.Direct3D11.Texture2D(device, bufferDesc);
            device.Copy(emptyTexture, localbuffer);
            return localbuffer;
        }

        private void FillMipSlice(SharpDX.Toolkit.Graphics.GraphicsDevice device, Material computeShader, int textureSize, UnorderedAccessView UAVMip, Vector4 fillColour)
        {
            computeShader.SetParameterResource("gOutput", UAVMip);
            computeShader.SetParameterValue("fillColour", fillColour);
            computeShader.Apply();
            device.Dispatch(1, textureSize, 1);
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
            desc.Width = textureSize;
            desc.Height = textureSize;
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
            desc.Width = textureSize;
            desc.Height = textureSize;
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
