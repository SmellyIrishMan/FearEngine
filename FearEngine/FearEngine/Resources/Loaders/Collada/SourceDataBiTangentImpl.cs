using FearEngine.Resources.Meshes;
using grendgine_collada;
using SharpDX;

namespace FearEngine.Resources.Managment.Loaders.Collada
{
    class SourceDataBiTangentImpl : SourceData
    {
        public SourceDataBiTangentImpl(Grendgine_Collada_Source src) : base(src) { }

        override public VertexInfoType GetVertInfoType()
        {
            return VertexInfoType.BITANGENT;
        }
    }
}
