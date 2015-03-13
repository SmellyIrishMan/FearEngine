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
        void Load(string filename);

        void GetResourceType();

        object GetResource(string filename);
        //So in the manager we'll have a call like GetMaterial, GetTexture, GetSound etc...
        //So that will call the appropriate loader and then cast the result to the correct type? Does that make sense? Let's hope so.
    }
}
