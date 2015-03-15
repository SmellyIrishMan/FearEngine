using FearEngine.Resources.Managment;
using FearEngine.Resources.Managment.Loaders;
using System.Drawing;

namespace FearEngine.Resources.Loaders
{
    public class TextureLoader : ResourceLoader
    {
        SharpDX.Toolkit.Graphics.GraphicsDevice device;

        public TextureLoader(SharpDX.Toolkit.Graphics.GraphicsDevice dev)
        {
            device = dev;
        }

        public Resource Load(ResourceInformation info)
        {
            SharpDX.Toolkit.Graphics.Texture2D texture = SharpDX.Toolkit.Graphics.Texture2D.Load(device, info.GetFilepath());
            SharpDX.Direct3D11.ShaderResourceView textureView = new SharpDX.Direct3D11.ShaderResourceView(device, texture);

            Texture fearTexture = new Texture(texture, textureView);
            return fearTexture;
        }
    }
}
