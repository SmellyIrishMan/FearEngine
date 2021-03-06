﻿using FearEngine.Resources.Meshes;
using grendgine_collada;

namespace FearEngine.Resources.Loaders.Loaders.Collada
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
