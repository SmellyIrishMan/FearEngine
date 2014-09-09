using System.Windows.Forms;
using SharpDX;

namespace FearEngine
{
    public class Transform
    {
        public Vector3 Position { get; private set; }
        public Vector3 Forward { get; private set; }
        public Vector3 Right { get; private set; }
        public Vector3 Up { get; private set; }

        private Quaternion rotation;

        public Transform()
        {
            rotation = Quaternion.Identity;
            Position = Vector3.Zero;
            Forward = Vector3.UnitZ;
            Right = Vector3.UnitX;
            Up = Vector3.UnitY;
        }

        public void MoveTo(Vector3 position)
        {
            Position = position;
        }

        public void Pitch(float radians)
        {
            rotation.X += radians;

            Right = Vector3.Transform(Vector3.UnitX, rotation);
            Up = Vector3.Transform(Vector3.UnitY, rotation);
            Forward = Vector3.Transform(Vector3.UnitZ, rotation);
        }

        public void Yaw(float radians)
        {
            rotation.Y += radians;

            Right = Vector3.Transform(Vector3.UnitX, rotation);
            Up = Vector3.Transform(Vector3.UnitY, rotation);
            Forward = Vector3.Transform(Vector3.UnitZ, rotation);
        }
    }
}