﻿using FearEngine.Resources.Management;
using FearEngine.Resources.ResourceFiles.ResourceFileInformation;

namespace FearEngine.Resources.Loaders
{
    public class TextureLoader : ResourceLoader
    {
        SharpDX.Toolkit.Graphics.GraphicsDevice device;

        public TextureLoader(FearGraphicsDevice dev)
        {
            device = dev.Device;
        }

        public Resource Load(ResourceInformation info)
        {
            SharpDX.Toolkit.Graphics.Texture2D texture = SharpDX.Toolkit.Graphics.Texture2D.Load(device, info.Filepath);
            SharpDX.Direct3D11.ShaderResourceView textureView;

            bool isLinearData = info.GetBool("IsLinear");
            if (isLinearData)
            {
                textureView = new SharpDX.Direct3D11.ShaderResourceView(device, texture);
            }
            else            
            {
                SharpDX.Direct3D11.ImageLoadInformation imageInfo = new SharpDX.Direct3D11.ImageLoadInformation();

                imageInfo.BindFlags = SharpDX.Direct3D11.BindFlags.ShaderResource;
                imageInfo.Format = SharpDX.DXGI.Format.R8G8B8A8_UNorm_SRgb;
                imageInfo.Filter = SharpDX.Direct3D11.FilterFlags.SRgb | SharpDX.Direct3D11.FilterFlags.None;

                SharpDX.Direct3D11.Resource sRGBTexture = SharpDX.Direct3D11.Texture2D.FromFile(device, info.Filepath, imageInfo);
                textureView = new SharpDX.Direct3D11.ShaderResourceView(device, sRGBTexture);
            }

            Texture fearTexture = new Texture(texture, textureView);
            return fearTexture;
        }
    }
}
