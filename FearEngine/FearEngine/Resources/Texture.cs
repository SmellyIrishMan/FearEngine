﻿using FearEngine.Resources.Management;
using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources
{
    public class Texture : Resource
    {
        private Texture2D texture;
        private SharpDX.Direct3D11.ShaderResourceView textureView;

        public Texture(Texture2D tex, SharpDX.Direct3D11.ShaderResourceView texView)
        {
            texture = tex;
            textureView = texView;
        }

        public bool IsLoaded()
        {
            return true;
        }

        public void Dispose()
        {
            texture.Dispose();
            textureView.Dispose();
        }

        public int Width
        {
            get { return texture.Width; }
        }

        public int Height
        {
            get { return texture.Height; }
        }

        public SharpDX.Direct3D11.ShaderResourceView ShaderView 
        {
            get { return textureView; }
        }
    }
}
