using FearEngine.Resources.Managment.Loaders;
using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources.Managment
{
    public interface ResourceLoader
    {
        Resource Load(ResourceInformation info);
    }
}
