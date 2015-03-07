using grendgine_collada;
using SharpDX;

namespace FearEngine.Resources.Managment.Loaders.Collada
{
    class SourceDataUnknownImpl : SourceData
    {
        public SourceDataUnknownImpl() { }

        override public SourceType GetSourceType()
        {
            return SourceType.UNKNOWN;
        }
    }
}
