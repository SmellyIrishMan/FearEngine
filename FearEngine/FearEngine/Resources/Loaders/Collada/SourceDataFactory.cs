using FearEngine.Resources.Meshes;
using grendgine_collada;
using System;
using System.Collections.Generic;

namespace FearEngine.Resources.Managment.Loaders.Collada
{
    class SourceDataFactory
    {
        private Dictionary<VertexInfoType, List<String>> sourceTypeToString;

        public SourceDataFactory()
        {
            sourceTypeToString = new Dictionary<VertexInfoType, List<String>>();
            populateSourceToStringTable();
        }

        private void populateSourceToStringTable()
        {
            sourceTypeToString[VertexInfoType.POSITION] = new List<string>(new String[] {
                "position" });

            sourceTypeToString[VertexInfoType.NORMAL] = new List<string>(new String[] {
                "normal" });

            sourceTypeToString[VertexInfoType.TEXCOORD1] = new List<string>(new String[] {
                "map1" });

            sourceTypeToString[VertexInfoType.TEXCOORD2] = new List<string>(new String[] {
                "map2" });
        }

        public SourceData CreateSourceData(Grendgine_Collada_Source src)
        {
            SourceData data;
            VertexInfoType srcType = getSourceTypeFromId(src.ID);
            switch (srcType)
            {
                case VertexInfoType.POSITION:
                    data = new SourceDataPositionImpl(src);
                    break;
                case VertexInfoType.NORMAL:
                    data = new SourceDataNormalImpl(src);
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

        private VertexInfoType getSourceTypeFromId(string scrId)
        {
            foreach (VertexInfoType type in sourceTypeToString.Keys)
            {
                foreach (String matchString in sourceTypeToString[type])
                {
                    if (scrId.IndexOf(matchString, StringComparison.CurrentCultureIgnoreCase) > -1)
                    {
                        return type;
                    }
                }
            }

            throw new UnknownSourceTypeException();
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
