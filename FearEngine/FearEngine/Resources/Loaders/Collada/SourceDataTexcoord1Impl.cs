using FearEngine.Resources.Meshes;
using grendgine_collada;
using SharpDX;

namespace FearEngine.Resources.Loaders.Loaders.Collada
{
    class SourceDataTexcoord1Impl : SourceData
    {
        public SourceDataTexcoord1Impl(Grendgine_Collada_Source src) : base(src) { }

        override public VertexInfoType GetVertInfoType()
        {
            return VertexInfoType.TEXCOORD1;
        }
    }
}
