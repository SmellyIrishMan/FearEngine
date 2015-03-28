using FearEngine.Logger;
using FearEngine.Resources.Meshes;
using grendgine_collada;
using SharpDX;
using System;
using System.Collections.Generic;

namespace FearEngine.Resources.Managment.Loaders.Collada
{
    public class ColladaMeshLoader
    {
        private const bool switchFromZUpToYUp = true;
        Grendgine_Collada_Mesh meshData;

        UInt32[] indices;
        VertexInformation[] vertices;

        public ColladaMeshLoader()
        {

        }

        public MeshInformation Load(string filename) 
        {
            if (!IsFileValid(filename)){
                return new MeshInformation();
            }

            meshData = Grendgine_Collada.Grendgine_Load_File(filename).Library_Geometries.Geometry[0].Mesh;

            BuildIndicesArray();
            BuildVertexArray();

            SwitchUpAxisFromZtoY();

            return new MeshInformation(vertices, indices);
        }

        private bool IsFileValid(string filename)
        {
            return System.IO.File.Exists(filename);
        }

        private void BuildIndicesArray()
        {
            indices = new UInt32[GetNumberOfVerticesInMesh()];
            for (int i = 0; i < indices.Length; i++) { indices[i] = (uint)i; }
        }

        private uint GetNumberOfVerticesInMesh()
        {
            return (uint)meshData.Triangles[0].Count * (uint)meshData.Triangles[0].Input.Length;
        }

        private void BuildVertexArray()
        {
            List<SourceData> sourceData = LoadSourceData();

            PrepareVertices(sourceData);
            PopulateVerticesInfo(sourceData);
        }

        private void PrepareVertices(List<SourceData> sourceData)
        {
            List<VertexInfoType> inputs = new List<VertexInfoType>();
            foreach (SourceData source in sourceData)
            {
                inputs.Add(source.GetVertInfoType());
            }

            vertices = new VertexInformation[GetNumberOfVerticesInMesh()];
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = new VertexInformation(inputs);
            }
        }

        private void PopulateVerticesInfo(List<SourceData> sourceData)
        {
            VertexInfoType inputType;
            int stride = 0;
            int[] triangles = meshData.Triangles[0].P.Value();
            int numOfTriangles = triangles.Length / sourceData.Count;

            for (int vertexIndex = 0; vertexIndex < numOfTriangles; vertexIndex++)
            {
                stride = vertexIndex * sourceData.Count;
                foreach (SourceData source in sourceData)
                {
                    inputType = source.GetVertInfoType();
                    vertices[vertexIndex].SetValue(inputType, source.Data[triangles[stride + GetOffsetForTriangleSourceInput(inputType)]]);
                }
            }
        }

        private int GetOffsetForTriangleSourceInput(VertexInfoType inputType)
        {
            foreach (Grendgine_Collada_Input_Shared inp in meshData.Triangles[0].Input)
            {
                if (VertexInformation.MapSemanticStringToVertexInfoType(inp.Semantic.ToString()) == inputType)
                {
                    return inp.Offset;
                }
            }

            FearLog.Log("Could not find an offset for the inputType " + inputType.ToString(), LogPriority.ALWAYS);
            return 0;
        }

        private List<SourceData> LoadSourceData()
        {
            List<SourceData> sourceData = new List<SourceData>();

            SourceDataFactory soureDataFactory = new SourceDataFactory();
            foreach (Grendgine_Collada_Source source in meshData.Source)
            {
                sourceData.Add(soureDataFactory.CreateSourceData(source));
            }

            return sourceData;
        }

        private void SwitchUpAxisFromZtoY()
        {
            Matrix adjustAxis = Matrix.RotationX(SharpDX.MathUtil.DegreesToRadians(-90));
            for (int vertexIndex = 0; vertexIndex < GetNumberOfVerticesInMesh(); vertexIndex++)
            {
                vertices[vertexIndex].SetValue(VertexInfoType.POSITION, Vector3.TransformNormal(vertices[vertexIndex].GetValue(VertexInfoType.POSITION), adjustAxis));
                vertices[vertexIndex].SetValue(VertexInfoType.NORMAL, Vector3.TransformNormal(vertices[vertexIndex].GetValue(VertexInfoType.NORMAL), adjustAxis));
                vertices[vertexIndex].GetValue(VertexInfoType.NORMAL).Normalize();
            }
        }
    }
}