using grendgine_collada;
using SharpDX;

namespace FearEngine.Resources.Managment.Loaders.Collada
{
    class SourceDataNormalImpl : SourceData
    {
        public SourceDataNormalImpl(Grendgine_Collada_Source src) : base(src) { }

        override public SourceType GetSourceType()
        {
            return SourceType.NORMAL;
        }
    }
}
