﻿using grendgine_collada;
using SharpDX;

namespace FearEngine.Resources.Managment.Loaders.Collada
{
    class SourceDataTexcoord1Impl : SourceData
    {
        public SourceDataTexcoord1Impl(Grendgine_Collada_Source src) : base(src) { }

        override public SourceType GetSourceType()
        {
            return SourceType.TEXCOORD1;
        }
    }
}