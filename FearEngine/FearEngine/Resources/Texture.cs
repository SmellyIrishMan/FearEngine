using FearEngine.Resources.Managment;
using System.Drawing;

namespace FearEngine.Resources
{
    public class Texture : Resource
    {
        Bitmap bitmap;

        public Texture(Bitmap map)
        {
            bitmap = map;
        }

        public bool IsLoaded()
        {
            return true;
        }
    }
}
