using grendgine_collada;
using System;
using System.Collections.Generic;

namespace FearEngine.Resources.Managment.Loaders.Collada
{
    class SourceDataFactory
    {
        private Dictionary<SourceType, List<String>> sourceTypeToString;

        public SourceDataFactory()
        {
            sourceTypeToString = new Dictionary<SourceType, List<String>>();
            populateSourceToStringTable();
        }

        private void populateSourceToStringTable()
        {
            sourceTypeToString[SourceType.POSITION] = new List<string>(new String[] {
                "position" });

            sourceTypeToString[SourceType.NORMAL] = new List<string>(new String[] {
                "normal" });

            sourceTypeToString[SourceType.TEXCOORD1] = new List<string>(new String[] {
                "map1" });

            sourceTypeToString[SourceType.TEXCOORD2] = new List<string>(new String[] {
                "map2" });
        }

        public SourceData CreateSourceData(Grendgine_Collada_Source src)
        {
            SourceData data;
            SourceType srcType = getSourceTypeFromId(src.ID);
            switch (srcType)
            {
                case SourceType.POSITION:
                    data = new SourceDataPositionImpl(src);
                    break;
                case SourceType.NORMAL:
                    data = new SourceDataNormalImpl(src);
                    break;
                case SourceType.TEXCOORD1:
                    data = new SourceDataTexcoord1Impl(src);
                    break;
                case SourceType.TEXCOORD2:
                    data = new SourceDataTexcoord2Impl(src);
                    break;
                default:
                    data = new SourceDataUnknownImpl();
                    break;
            }

            return data;
        }

        private SourceType getSourceTypeFromId(string scrId)
        {
            foreach (SourceType type in sourceTypeToString.Keys)
            {
                foreach (String matchString in sourceTypeToString[type])
                {
                    if (scrId.IndexOf(matchString, StringComparison.CurrentCultureIgnoreCase) > -1)
                    {
                        return type;
                    }
                }
            }
            return SourceType.UNKNOWN;
        }
    }
}
