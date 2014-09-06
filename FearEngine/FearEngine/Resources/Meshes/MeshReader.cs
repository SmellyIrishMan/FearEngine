using FearEngine.Logger;
using grendgine_collada;
using SharpDX;
using System;

namespace FearEngine.Resources.Meshes
{
    public class MeshReader
    {
        private const string meshExtension = "DAE";
        private const bool switchFromZUpToYUp = true;

        public MeshReader()
        {}

        //TODO HOLY FUCKING SHIT THIS IS HORRENDOUS
        public Mesh LoadMesh(string filename)
        {
            if(filename.Substring(filename.LastIndexOf('.') + 1).ToUpper().CompareTo(meshExtension) != 0)
            {
                FearLog.Log("Unknown file extension for mesh.", LogPriority.HIGH);
                return null;
            }
            else
            {
                Grendgine_Collada check  = Grendgine_Collada.Grendgine_Load_File(filename);
                Grendgine_Collada_Mesh mesh = Grendgine_Collada.Grendgine_Load_File(filename).Library_Geometries.Geometry[0].Mesh;

                uint numberOfVertices = (uint)mesh.Triangles[0].Count * (uint)mesh.Triangles[0].Input.Length;
                //TODO Why is this the same as the number of verts? It shouldn't be!
                UInt32[] indices = new UInt32[numberOfVertices];
                for (int i = 0; i < numberOfVertices; i++) { indices[i] = (uint)i; }

                VertexLayouts.PositionNormalTexture[] vertices = new VertexLayouts.PositionNormalTexture[numberOfVertices];

                Vector3[] positions = ReadSource(mesh.Source[0]);
                Vector3[] normals = ReadSource(mesh.Source[1]);
                Vector3[] textures = ReadSource(mesh.Source[2]);

                int[] triangles = mesh.Triangles[0].P.Value();
                Matrix adjustAxis = Matrix.RotationX(SharpDX.MathUtil.DegreesToRadians(-90));

                Vector2 newTexcoord;
                int adjustedIndex = 0;
                for (int i = 0; i < (triangles.Length / 3); i ++)
                {
                    adjustedIndex = i * 3;
                    vertices[i].Position = positions[triangles[adjustedIndex]];
                    vertices[i].Normal = normals[triangles[adjustedIndex + 1]];

                    if (switchFromZUpToYUp)
                    {
                        vertices[i].Position = Vector3.TransformNormal(vertices[i].Position, adjustAxis);
                        vertices[i].Normal = Vector3.TransformNormal(vertices[i].Normal, adjustAxis);
                        vertices[i].Normal.Normalize();
                    }
                    newTexcoord.X = textures[triangles[adjustedIndex + 2]].X;
                    newTexcoord.Y = textures[triangles[adjustedIndex + 2]].Y;
                    vertices[i].TexCoord = newTexcoord;
                }

                 return new Mesh(vertices, indices);
            }
        }

        private Vector3[] ReadSource(Grendgine_Collada_Source src)
        {
            uint positionCount = src.Technique_Common.Accessor.Count;
            uint positionStride = src.Technique_Common.Accessor.Stride;

            float[] values = src.Float_Array.Value();
            uint valueOffset = 0;

            Vector3[] extractedValues = new Vector3[src.Technique_Common.Accessor.Count];
            for (uint i = 0; i < positionCount; i++)
            {
                extractedValues[i].X = values[valueOffset];
                extractedValues[i].Y = values[valueOffset + 1];
                extractedValues[i].Z = values[valueOffset + 2];
                valueOffset += positionStride;
            }

            return extractedValues;
        }
    }
}
