using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace FearEngine.Meshes.Primatives
{
    class Plane
    {
        Vector2 Size { get; set; }  //How many points in the plane? 
        Vector2 Scale { get; set; } //How far between points?

        Buffer VertexBuffer { get; set; }
        Buffer IndexBuffer { get; set; }

        public Plane()
        {
            Size = new Vector2(100, 100);
            Scale = new Vector2(10, 10);
        }
    }
}
