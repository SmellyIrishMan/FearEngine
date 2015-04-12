using FearEngine.Resources.Meshes;
using grendgine_collada;
using SharpDX;

namespace FearEngine.Resources.Loaders.Loaders.Collada
{
    class SourceDataTexcoord2Impl : SourceData
    {
        public SourceDataTexcoord2Impl(Grendgine_Collada_Source src) : base(src) { }

        override public VertexInfoType GetVertInfoType()
        {
            return VertexInfoType.TEXCOORD2;
        }
    }
}
