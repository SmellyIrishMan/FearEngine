using grendgine_collada;
using SharpDX;
using System;


namespace FearEngine.Resources.Managment.Loaders.Collada
{
    enum SourceType
    {
        POSITION,
        NORMAL,
        TEXCOORD1,
        TEXCOORD2
    }

    abstract class SourceData
    {
        protected Vector3[] sourceData;

        public SourceData()
        {
            sourceData = new Vector3[0];
        }

        public SourceData(Grendgine_Collada_Source src)
        {
            uint positionCount = src.Technique_Common.Accessor.Count;
            uint positionStride = src.Technique_Common.Accessor.Stride;

            float[] values = src.Float_Array.Value();
            uint valueOffset = 0;

            sourceData = new Vector3[src.Technique_Common.Accessor.Count];
            for (uint i = 0; i < positionCount; i++)
            {
                sourceData[i].X = values[valueOffset];
                sourceData[i].Y = values[valueOffset + 1];
                sourceData[i].Z = values[valueOffset + 2];
                valueOffset += positionStride;
            }
        }

        public Vector3[] GetData()
        {
            return sourceData;
        }

        abstract public SourceType GetSourceType();
    }
}
