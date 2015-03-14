using FearEngine.Logger;
using FearEngine.Resources.Managment.Loaders;
using FearEngine.Resources.Managment.Loaders.Collada;
using FearEngine.Resources.Meshes;
using SharpDX.Toolkit.Graphics;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;

namespace FearEngine.Resources.Managment
{
    public interface ResourceLoader
    {
        Resource Load(ResourceInformation info);
    }
}
