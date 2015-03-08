﻿using grendgine_collada;
using SharpDX;

namespace FearEngine.Resources.Managment.Loaders.Collada
{
    class SourceDataPositionImpl : SourceData
    {
        public SourceDataPositionImpl(Grendgine_Collada_Source src) : base(src){}

        override public SourceType GetSourceType()
        {
            return SourceType.POSITION;
        }
    }
}