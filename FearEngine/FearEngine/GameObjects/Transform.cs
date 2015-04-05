using System.Windows.Forms;
using SharpDX;

namespace FearEngine.GameObjects
{
    public class Transform
    {
        Quaternion rotation;

        Vector3 scale;
        
        Vector3 position;
        Vector3 forward;
        Vector3 right;
        Vector3 up;

        public Quaternion Rotation { get { return rotation; } }
        public Vector3 Scale { get { return scale; } }
        public Vector3 Position { get { return position; } }
        public Vector3 Forward { get { return forward; } }
        public Vector3 Right { get { return right; } }
        public Vector3 Up { get { return up; } }

        public delegate void TransformChangedHandler(Transform newTransform);

        public event TransformChangedHandler Changed;

        public Transform()
        {
            rotation = Quaternion.Identity;

            scale = Vector3.One;

            position = Vector3.Zero;
            forward = Vector3.ForwardLH;
            right = Vector3.Right;
            up = Vector3.Up;
        }

        public void MoveTo(Vector3 pos)
        {
            position = pos;
            NotifyNewTransform();
        }

        public void Rotate(Quaternion quaternion)
        {
            rotation = rotation * quaternion;
        }

        public void SetRotation(Quaternion quat)
        {
            rotation = quat;
            UpdateLocalDirections();
            NotifyNewTransform();
        }

        private void UpdateLocalDirections()
        {
            right = Vector3.Transform(Vector3.Right, Rotation);
            up = Vector3.Transform(Vector3.Up, Rotation);
            forward = Vector3.Transform(Vector3.ForwardLH, Rotation);
        }

        private void NotifyNewTransform()
        {
            if (Changed != null)
            {
                Changed(this);
            }
        }

        public void SetScale(float p)
        {
            scale = new Vector3(p);
        }
    }
}