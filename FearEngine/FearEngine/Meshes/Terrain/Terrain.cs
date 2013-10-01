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

        public Terrain(Point size, Bitmap heights) : base(size)
        {
            heightmap = heights;
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

        //The normals are computed using the following technique
        //http://www.gamedev.net/page/resources/_/technical/graphics-programming-and-theory/efficient-normal-computations-for-terrain-lighting-in-directx-10-r3313
        private void CalculateNormals()
        {
	        int i, j, leftVert, rightVert, lowerVert, upperVert, currentVert;
	        Vector3 vertex1, vertex2, vertex3, vector1, vector2, sum;
            Vector3[] normals = new Vector3[Size.Y * Size.X];

	        // Go through all the faces in the mesh and calculate their normals.
            for (j = 0; j < Size.Y - 1; j++)
	        {
                for (i = 0; i < Size.X; i++)
		        {
                    currentVert = (j * Size.X) + i;
                    leftVert = (j * Size.X) + (i - 1);
                    rightVert = (j * Size.X) + (i + 1);
                    lowerVert = ((j - 1) * Size.X) + i;
                    upperVert = ((j + 1) * Size.X) + i;

                    Vector3 normal = new Vector3(0, 1, 0);
                    if (j == 0) //If our current pixel is on the bottom row so it has no low pixel to bounce off.
                    {
                        normal.Z = Vertices[currentVert].Position.Y - Vertices[upperVert].Position.Y;
                    }
                    else if (j == Size.Y)   //If our current pixel is on the top row so it has no higher pixel to bounce off.
                    {
                        normal.Z = Vertices[lowerVert].Position.Y - Vertices[currentVert].Position.Y;
                    }
                    else
                    {
                        normal.Z = ((Vertices[currentVert].Position.Y - Vertices[upperVert].Position.Y) 
                            + (Vertices[lowerVert].Position.Y - Vertices[currentVert].Position.Y)) * 0.5f;
                    }

                    if (i == 0)
                    {
                        normal.X = Vertices[currentVert].Position.Y - Vertices[rightVert].Position.Y;
                    }
                    else if (i == Size.X)
                    {
                        normal.X = Vertices[leftVert].Position.Y - Vertices[currentVert].Position.Y;
                    }
                    else
                    {
                        normal.X = ((Vertices[currentVert].Position.Y - Vertices[rightVert].Position.Y) 
                            + (Vertices[leftVert].Position.Y - Vertices[currentVert].Position.Y)) * 0.5f;
                    }

			        // Calculate the cross product of those two vectors to get the un-normalized value for this face normal.
                    Vertices[currentVert].Normal = normals[currentVert] = Vector3.Normalize(normal);
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
                        currentVert = ((j - 1) * Size.X) + (i - 1);

                        sum += normals[currentVert];
                    }

                    // Bottom right face.
                    if ((i < (Size.X - 1)) && ((j - 1) >= 0))
                    {
                        currentVert = ((j - 1) * Size.X) + (i + 1);

                        sum += normals[currentVert];
                    }

                    // Upper left face.
                    if (((i - 1) >= 0) && (j < (Size.Y - 1)))
                    {
                        currentVert = ((j + 1) * Size.X) + (i - 1);

                        sum += normals[currentVert];
                    }

                    // Upper right face.
                    if ((i < (Size.X - 1)) && (j + 1 < Size.Y))
                    {
                        currentVert = ((j + 1) * Size.X) + i;

                        sum += normals[currentVert];
                    }

                    // Get an index to the vertex location in the height map array.
                    currentVert = (j * Size.X) + i;

                    // Normalize the final shared normal for this vertex and store it in the height map array.
                    Vertices[currentVert].Normal = Vector3.Normalize(sum);
                }
            }
        }
    }
}
