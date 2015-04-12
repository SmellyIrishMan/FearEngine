using FearEngine.Resources.Meshes;
using grendgine_collada;
using SharpDX;

namespace FearEngine.Resources.Loaders.Loaders.Collada
{
    class SourceDataTangentImpl : SourceData
    {
        public SourceDataTangentImpl(Grendgine_Collada_Source src) : base(src) { }

        override public VertexInfoType GetVertInfoType()
        {
            return VertexInfoType.TANGENT;
        }
    }
}
