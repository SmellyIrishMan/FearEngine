using FearEngine.GameObjects;
using Ninject;
using SharpDX;

namespace FearEngine.Cameras
{
    public class FearCamera : Camera
    {
        private Transform transform;

        public Matrix View { get; private set; }
        public Matrix Projection { get; private set; }
        public Vector3 Position { get { return transform.Position; } }

        public FearCamera(Transform trans)
        {
            AttachToTransform(trans);

            Projection = Matrix.PerspectiveFovLH(SharpDX.MathUtil.Pi * 0.25f, 1280.0f/720.0f, 0.01f, 1000.0f);
        }

        public void AdjustProjection(float fov, float aspect, float near, float far)
        {
            Projection = Matrix.PerspectiveFovLH(SharpDX.MathUtil.Pi * 0.25f, 1280.0f/720.0f, 0.01f, 1000.0f);
        }

        public void AttachToTransform(Transform trans)
        {
            RemoveOldListener();

            transform = trans;

            transform.Changed += OnTransformChanged;

            OnTransformChanged(transform);
        }

        private void RemoveOldListener()
        {
            if (transform != null)
            {
                transform.Changed -= OnTransformChanged;
            }
        }

        void OnTransformChanged(Transform newTransform)
        {
            View = Matrix.LookAtLH(newTransform.Position, newTransform.Position + newTransform.Forward, Vector3.UnitY);
        }
    }
}
