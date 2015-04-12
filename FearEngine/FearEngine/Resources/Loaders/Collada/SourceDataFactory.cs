using FearEngine.Resources.Meshes;
using grendgine_collada;
using System;
using System.Collections.Generic;

namespace FearEngine.Resources.Loaders.Loaders.Collada
{
    class SourceDataFactory
    {
        private Dictionary<VertexInfoType, List<String>> sourceTypeToString;

        public SourceDataFactory()
        {
            sourceTypeToString = new Dictionary<VertexInfoType, List<String>>();
        }

        public SourceData CreateSourceData(Grendgine_Collada_Source src, VertexInfoType vertType)
        {
            SourceData data;
            switch (vertType)
            {
                case VertexInfoType.POSITION:
                    data = new SourceDataPositionImpl(src);
                    break;
                case VertexInfoType.NORMAL:
                    data = new SourceDataNormalImpl(src);
                    break;
                case VertexInfoType.TANGENT:
                    data = new SourceDataTangentImpl(src);
                    break;
                case VertexInfoType.BITANGENT:
                    data = new SourceDataBiTangentImpl(src);
                    break;
                case VertexInfoType.TEXCOORD1:
                    data = new SourceDataTexcoord1Impl(src);
                    break;
                case VertexInfoType.TEXCOORD2:
                    data = new SourceDataTexcoord2Impl(src);
                    break;
                default:
                    throw new UnknownSourceTypeException();
            }

            return data;
        }

        public class UnknownSourceTypeException : Exception
        {
            public UnknownSourceTypeException()
            {
            }

            public UnknownSourceTypeException(string message)
                : base(message)
            {
            }

            public UnknownSourceTypeException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
    }
}
