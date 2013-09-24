using System;
using System.Collections.Generic;
using System.Drawing;
using FearEngine.Meshes.Primatives;
using SharpDX;
namespace FearEngine.Meshes.Terrain
{
    public class Terrain : FearEngine.Meshes.Primatives.Plane
    {
        Bitmap heightmap;
        private const float TERRAIN_HEIGHT = 45.0f;

        public Terrain(Point size, string heightmapLocation) : base(size)
        {
            heightmap = new Bitmap("..\\..\\..\\..\\..\\Resources\\Textures\\Heightmaps\\Heightmap.bmp");
            heightmap = new Bitmap(heightmapLocation);
        }

        override protected void InitialiseVerticesAndIndicies()
        {
            UInt32 index = 0;
            for (int j = 0; j < Size.Y; j++)
            {
                for (int i = 0; i < Size.X; i++)
                {
                    Vertices[index].Position = new Vector3(i, (heightmap.GetPixel(i, j).R / 255.0f) * TERRAIN_HEIGHT, j);
                    Vertices[index].Normal = new Vector3(0.0f, 1.0f, 0.0f);
                    index++;
                }
            }

            InitialiseIndices();

            CalculateNormals();
        }

        private void CalculateNormals()
        {
	        int i, j, index1, index2, index3, index;
	        Vector3 vertex1, vertex2, vertex3, vector1, vector2, sum;
            Vector3[] normals = new Vector3[Size.Y * Size.X];

	        // Go through all the faces in the mesh and calculate their normals.
            for (j = 0; j < Size.Y - 1; j++)
	        {
                for (i = 0; i < Size.X; i++)
		        {
                    index1 = (j * Size.X) + i;
                    index2 = (j * Size.X) + (i + 1);
                    index3 = ((j + 1) * Size.X) + i;

			        // Get three vertices from the face.
			        vertex1 = Vertices[index1].Position;
                    vertex2 = Vertices[index2].Position;
                    vertex3 = Vertices[index3].Position;

			        // Calculate the two vectors for this face.
                    vector1 = vertex1 - vertex3;
                    vector2 = vertex3 - vertex2;

                    index = (j * Size.X) + i;

			        // Calculate the cross product of those two vectors to get the un-normalized value for this face normal.
                    normals[index] = Vector3.Cross(vector1, vector2);

                    Vertices[index].Normal = Vector3.Cross(vector1, vector2);
		        }
	        }

	        // Now go through all the vertices and take an average of each face normal 	
	        // that the vertex touches to get the averaged normal for that vertex.
            for (j = 0; j < Size.Y; j++)
            {
                for (i = 0; i < Size.X; i++)
                {
                    // Initialize the sum.
                    sum = Vector3.Zero;

                    // Bottom left face.
                    if (((i - 1) >= 0) && ((j - 1) >= 0))
                    {
                        index = ((j - 1) * Size.X) + (i - 1);

                        sum += normals[index];
                    }

                    // Bottom right face.
                    if ((i < (Size.X - 1)) && ((j - 1) >= 0))
                    {
                        index = ((j - 1) * Size.X) + (i + 1);

                        sum += normals[index];
                    }

                    // Upper left face.
                    if (((i - 1) >= 0) && (j < (Size.Y - 1)))
                    {
                        index = ((j + 1) * Size.X) + (i - 1);

                        sum += normals[index];
                    }

                    // Upper right face.
                    if ((i < (Size.X - 1)) && (j + 1 < Size.Y))
                    {
                        index = ((j + 1) * Size.X) + i;

                        sum += normals[index];
                    }

                    // Get an index to the vertex location in the height map array.
                    index = (j * Size.X) + i;

                    // Normalize the final shared normal for this vertex and store it in the height map array.
                    Vertices[index].Normal = Vector3.Normalize(sum);
                }
            }
        }
    }
}
