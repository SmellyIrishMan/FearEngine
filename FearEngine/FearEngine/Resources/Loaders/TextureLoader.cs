using FearEngine.Resources.Managment;
using FearEngine.Resources.Managment.Loaders;
using SharpDX.Toolkit.Graphics;
using System.Drawing;

namespace FearEngine.Resources.Loaders
{
    public class TextureLoader : ResourceLoader
    {
        GraphicsDevice device;

        public TextureLoader(GraphicsDevice dev)
        {
            device = dev;
        }

        public Resource Load(ResourceInformation info)
        {
            Texture texture = new Texture(new Bitmap(info.GetFilepath()));
            return texture;
        }
    }
}
