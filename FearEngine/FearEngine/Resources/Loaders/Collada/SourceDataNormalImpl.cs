using FearEngine.Resources.Meshes;
using grendgine_collada;

namespace FearEngine.Resources.Loaders.Loaders.Collada
{
    class SourceDataNormalImpl : SourceData
    {
        public SourceDataNormalImpl(Grendgine_Collada_Source src) : base(src) { }

        override public VertexInfoType GetVertInfoType()
        {
            return VertexInfoType.NORMAL;
        }
    }
}
