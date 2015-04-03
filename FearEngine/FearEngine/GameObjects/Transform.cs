using System.Windows.Forms;
using SharpDX;

namespace FearEngine.GameObjects
{
    public class Transform
    {
        public Quaternion Rotation { get; private set; }
        public Vector3 Position { get; private set; }
        public Vector3 Forward { get; private set; }
        public Vector3 Right { get; private set; }
        public Vector3 Up { get; private set; }

        private Vector3 YawPitchRoll;

        public delegate void TransformChangedHandler(Transform newTransform);

        public event TransformChangedHandler Changed;

        public Transform()
        {
            Rotation = Quaternion.Identity;
            Position = Vector3.Zero;
            Forward = Vector3.ForwardLH;
            Right = Vector3.Right;
            Up = Vector3.Up;
        }

        public void MoveTo(Vector3 position)
        {
            Position = position;
            NotifyNewTransform();
        }

        public void Pitch(float radians)
        {
            YawPitchRoll.Y += radians;
            UpdateRotation();
            NotifyNewTransform();
        }

        public void Yaw(float radians)
        {
            YawPitchRoll.X += radians;
            UpdateRotation();
            NotifyNewTransform();
        }

        private void UpdateRotation()
        {
            Rotation = Quaternion.RotationYawPitchRoll(YawPitchRoll.X, YawPitchRoll.Y, YawPitchRoll.Z);
            Right = Vector3.Transform(Vector3.Right, Rotation);
            Up = Vector3.Transform(Vector3.Up, Rotation);
            Forward = Vector3.Transform(Vector3.ForwardLH, Rotation);
        }

        private void NotifyNewTransform()
        {
            if (Changed != null)
            {
                Changed(this);
            }
        }
    }
}