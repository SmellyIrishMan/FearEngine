using System.Windows.Forms;
using SharpDX;

namespace FearEngine
{
    public class Transform
    {
        private const float STRAFE_SPEED = 0.1f;
        private const float WALK_SPEED = 0.1f;
        private const float ROTATION_SPEED = 0.0001f;

        public Vector3 Position { get; set; }

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
    }
}