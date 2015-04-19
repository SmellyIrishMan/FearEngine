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

        public void SetRotation(Quaternion quaternion)
        {
            rotation = SortOutQuaternion(quaternion);
            UpdateLocalDirections();
            NotifyNewTransform();
        }

        public void Rotate(Quaternion quaternion)
        {
            rotation = rotation * SortOutQuaternion(quaternion);
            UpdateLocalDirections();
            NotifyNewTransform();
        }

        private static Quaternion SortOutQuaternion(Quaternion quaternion)
        {
            quaternion.Normalize();
            return quaternion;
        }

        private void UpdateLocalDirections()
        {
            right = Vector3.Transform(Vector3.Right, rotation);
            right.Normalize();

            up = Vector3.Transform(Vector3.Up, rotation);
            up.Normalize();

            forward = Vector3.Transform(Vector3.ForwardLH, rotation);
            forward.Normalize();
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


        public void LookAt(Vector3 target)
        {
            Quaternion lookAt = Quaternion.LookAtLH(Position, target, Vector3.Up);
            lookAt.Conjugate();
            SetRotation(lookAt);
        }
    }
}