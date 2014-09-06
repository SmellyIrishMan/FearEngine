using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
namespace FearEngine.Resources.Meshes
{
    public static class VertexLayouts
    {
        public struct PositionColor
        {
            public Vector3 Position { get; set; }
            public Vector4 Color { get; set; }

            private static InputElement[] elements = new[]
            {
                new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 12, 0)
            };

            public static int GetByteSize()
            {
                int size = Utilities.SizeOf<Vector3>();
                size += Utilities.SizeOf<Vector4>();

                return size;
            }

            public static InputElement[] GetInputElements()
            {
                return elements;
            }
        }

        public struct PositionNormal
        {
            public Vector3 Position { get; set; }
            public Vector3 Normal { get; set; }

            private static InputElement[] elements = new[]
            {
                new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                new InputElement("NORMAL", 0, Format.R32G32B32_Float, 12, 0)
            };

            public static int GetByteSize()
            {
                int size = Utilities.SizeOf<Vector3>();
                size += Utilities.SizeOf<Vector3>();

                return size;
            }

            public static InputElement[] GetInputElements()
            {
                return elements;
            }
        }

        public struct PositionNormalTexture
        {
            public Vector3 Position { get; set; }
            public Vector3 Normal { get; set; }
            public Vector2 TexCoord { get; set; }

            private static InputElement[] elements = new[]
            {
                new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                new InputElement("NORMAL", 0, Format.R32G32B32_Float, 12, 0),
                new InputElement("TEXCOORD", 0, Format.R32G32_Float, 24, 0)
            };

            public static int GetByteSize()
            {
                int size = Utilities.SizeOf<Vector3>();
                size += Utilities.SizeOf<Vector3>();
                size += Utilities.SizeOf<Vector2>();

                return size;
            }

            public static InputElement[] GetInputElements()
            {
                return elements;
            }
        }
    }
}
