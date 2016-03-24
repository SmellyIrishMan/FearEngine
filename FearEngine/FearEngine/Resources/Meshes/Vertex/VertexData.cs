using SharpDX;
using System;
using System.Collections.Generic;

namespace FearEngine.Resources.Meshes
{
    public enum VertexInfoType
    {
        POSITION,
        NORMAL,
        TEXCOORD1,
        TEXCOORD2,
        TANGENT,
        BITANGENT,
    }

    public class VertexData
    {
        Dictionary<VertexInfoType, Vector3> inputs;
        public List<VertexInfoType> Inputs { get { return new List<VertexInfoType>(inputs.Keys); } }

        private static Dictionary<List<String>, VertexInfoType> SemanticNameToVertexInfoMap;

        public VertexData(List<VertexInfoType> vertexInputs)
        {
            inputs = new Dictionary<VertexInfoType, Vector3>();

            foreach (VertexInfoType input in vertexInputs)
            {
                inputs[input] = new Vector3();
            }
        }

        public void SetValue(VertexInfoType type, Vector3 value)
        {
            if (inputs.ContainsKey(type))
            {
                inputs[type] = value;
            }
        }

        public Vector3 GetValue(VertexInfoType type)
        {
            if (inputs.ContainsKey(type))
            {
                return inputs[type];
            }
            else
            {
                return Vector3.Zero;
            }
        }

        public static VertexInfoType MapSemanticStringToVertexInfoType(string semantic)
        {
            if (SemanticNameToVertexInfoMap == null)
            {
                CreateSemanticMapToVertexInfoTypeMap();
            }

            foreach (List<string> semanticNames in SemanticNameToVertexInfoMap.Keys)
            {
                if (semanticNames.Contains(semantic))
                {
                    return SemanticNameToVertexInfoMap[semanticNames];
                }
            }

            throw new SemanticNameNotFoundException();
        }

        private static void CreateSemanticMapToVertexInfoTypeMap()
        {
            SemanticNameToVertexInfoMap = new Dictionary<List<String>, VertexInfoType>();

            SemanticNameToVertexInfoMap.Add(new List<String>(new string[] {
                "VERTEX" }),
                VertexInfoType.POSITION);

            SemanticNameToVertexInfoMap.Add(new List<String>(new string[] {
                "NORMAL" }),
                VertexInfoType.NORMAL);

            SemanticNameToVertexInfoMap.Add(new List<String>(new string[] {
                "TEXCOORD" }),
                VertexInfoType.TEXCOORD1);

            SemanticNameToVertexInfoMap.Add(new List<String>(new string[] {
                "TEXTANGENT" }),
                VertexInfoType.TANGENT);

            SemanticNameToVertexInfoMap.Add(new List<String>(new string[] {
                "TEXBINORMAL" }),
                VertexInfoType.BITANGENT);
        }

        public class SemanticNameNotFoundException : Exception
        {
            public SemanticNameNotFoundException()
            {
            }

            public SemanticNameNotFoundException(string message)
                : base(message)
            {
            }

            public SemanticNameNotFoundException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
    }
}
