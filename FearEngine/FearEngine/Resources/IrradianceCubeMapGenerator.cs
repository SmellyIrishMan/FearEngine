using FearEngine.Resources.Materials;
using SharpDX.Direct3D11;

namespace FearEngine.Resources
{
    public class IrradianceCubeMapGenerator
    {
        SharpDX.Toolkit.Graphics.GraphicsDevice device;
        Material computeShader;
        int textureSize = 2048;
        int numOfMips = 6;
        int numOfSlices = 6;

        public IrradianceCubeMapGenerator(FearGraphicsDevice dev, Material compShader)
        {
            device = dev.Device;
            computeShader = compShader;
        }

        public ShaderResourceView GenerateIrradianceCubemapFromTextureCube(TextureCube originalCube)
        {
            Texture2DDescription emptyTextureDesc = CreateTextureDescription();
            Texture2D emptyTexture = new Texture2D(device, emptyTextureDesc);

            UnorderedAccessViewDescription uavDesc = CreateUAVDescription(0);
            UnorderedAccessView view = new UnorderedAccessView(device, emptyTexture, uavDesc);

            computeShader.SetParameterResource("gSource", originalCube.ShaderView);
            computeShader.SetParameterResource("gOutput", view);
            computeShader.Apply();

            int groups = textureSize / 64;
            device.Dispatch(groups, textureSize, numOfSlices);

            return null;
        }

        private Texture2DDescription CreateTextureDescription()
        {
            Texture2DDescription desc = new Texture2DDescription();
            desc.Width = textureSize;
            desc.Height = textureSize;
            desc.MipLevels = numOfMips;
            desc.ArraySize = numOfSlices;
            desc.Format = SharpDX.DXGI.Format.R8G8B8A8_UNorm;
            desc.SampleDescription.Count = 1;
            desc.SampleDescription.Quality = 0;
            desc.Usage = ResourceUsage.Default;
            desc.BindFlags = BindFlags.UnorderedAccess | BindFlags.ShaderResource;
            desc.CpuAccessFlags = CpuAccessFlags.None;
            desc.OptionFlags = ResourceOptionFlags.None;

            return desc;
        }

        private UnorderedAccessViewDescription CreateUAVDescription(int mipSlice)
        {
            UnorderedAccessViewDescription desc = new UnorderedAccessViewDescription();
            desc.Format = SharpDX.DXGI.Format.R8G8B8A8_UNorm;
            desc.Dimension = UnorderedAccessViewDimension.Texture2DArray;
            desc.Texture2DArray.ArraySize = numOfSlices;
            desc.Texture2DArray.FirstArraySlice = 0;
            desc.Texture2DArray.MipSlice = mipSlice;
            return desc;
        }
    }
}
