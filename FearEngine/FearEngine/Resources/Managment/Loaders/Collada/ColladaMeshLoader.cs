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
        VertexLayouts.PositionNormalTexture[] vertices;

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

            vertices = new VertexLayouts.PositionNormalTexture[GetNumberOfVerticesInMesh()];

            int[] triangles = meshData.Triangles[0].P.Value();

            Vector2 newTexcoord;
            int adjustedIndex = 0;
            int numOfTriangles = triangles.Length / sourceData.Count;
            for (int vertexIndex = 0; vertexIndex < numOfTriangles; vertexIndex++)
            {
                adjustedIndex = vertexIndex * sourceData.Count;
                vertices[vertexIndex].Position = sourceData[0].GetData()[triangles[adjustedIndex]];
                vertices[vertexIndex].Normal = sourceData[1].GetData()[triangles[adjustedIndex + 1]];


                newTexcoord.X = sourceData[2].GetData()[triangles[adjustedIndex + 2]].X;
                newTexcoord.Y = sourceData[2].GetData()[triangles[adjustedIndex + 2]].Y;
                vertices[vertexIndex].TexCoord = newTexcoord;
            }
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
                vertices[vertexIndex].Position = Vector3.TransformNormal(vertices[vertexIndex].Position, adjustAxis);
                vertices[vertexIndex].Normal = Vector3.TransformNormal(vertices[vertexIndex].Normal, adjustAxis);
                vertices[vertexIndex].Normal.Normalize();
            }
        }
    }
}