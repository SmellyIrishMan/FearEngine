using grendgine_collada;
using SharpDX;

namespace FearEngine.Resources.Managment.Loaders.Collada
{
    class SourceDataTexcoord2Impl : SourceData
    {
        public SourceDataTexcoord2Impl(Grendgine_Collada_Source src) : base(src) { }

        override public SourceType GetSourceType()
        {
            return SourceType.TEXCOORD2;
        }
    }
}
