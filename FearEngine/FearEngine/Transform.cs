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

        public Transform()
        {
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
            Matrix rotation = Matrix.RotationAxis(Right, radians);
            Up = Vector3.TransformNormal(Up, rotation);
            Forward = Vector3.TransformNormal(Forward, rotation);
        }

        public void Yaw(float radians)
        {
            Matrix rotation = Matrix.RotationY(radians);

            Up = Vector3.TransformNormal(Up, rotation);
            Right = Vector3.TransformNormal(Right, rotation);
            Forward = Vector3.TransformNormal(Forward, rotation);
        }
    }
}