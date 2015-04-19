using FearEngine.Resources.Management;
using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources
{
    public class TextureCube : Resource
    {
        private SharpDX.Toolkit.Graphics.TextureCube texCube;
        private SharpDX.Direct3D11.ShaderResourceView textureView;

        public TextureCube(SharpDX.Toolkit.Graphics.TextureCube cube, SharpDX.Direct3D11.ShaderResourceView texView)
        {
            texCube = cube;
            textureView = texView;
        }

        public bool IsLoaded()
        {
            return true;
        }

        public void Dispose()
        {
            texCube.Dispose();
            textureView.Dispose();
        }

        public int Width
        {
            get { return texCube.Width; }
        }

        public int Height
        {
            get { return texCube.Height; }
        }

        public SharpDX.Direct3D11.ShaderResourceView ShaderView
        {
            get { return textureView; }
        }
    }
}
