﻿using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources.Meshes
{
    class PNTTVertexBuffer
    {
        public Buffer CreateBufferFromMeshData(GraphicsDevice graphicsDevice, MeshData meshData)
        {
            uint vertexCount = meshData.GetVertexCount();
            VertexData[] data = meshData.GetVertexData();
            VertexLayouts.PositionNormalTangentTexture[] vertexData = new VertexLayouts.PositionNormalTangentTexture[vertexCount];

            for (int i = 0; i < vertexCount; i++)
            {
                vertexData[i].Position = data[i].GetValue(VertexInfoType.POSITION);
                vertexData[i].Normal = data[i].GetValue(VertexInfoType.NORMAL);
                vertexData[i].Tangent = data[i].GetValue(VertexInfoType.TANGENT);
                vertexData[i].TexCoord = ConvertVec3ToVec2ByDroppingZ(data[i].GetValue(VertexInfoType.TEXCOORD1));
            }

            return Buffer.Vertex.New(graphicsDevice, vertexData);
        }

        private SharpDX.Vector2 ConvertVec3ToVec2ByDroppingZ(SharpDX.Vector3 vector3)
        {
            return new SharpDX.Vector2(vector3.X, vector3.Y);
        }
    }
}
