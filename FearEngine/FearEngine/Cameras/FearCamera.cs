using FearEngine.GameObjects;
using Ninject;
using SharpDX;

namespace FearEngine.Cameras
{
    public class FearCamera : Camera
    {
        private GameObject gameObj;

        public Matrix View { get; private set; }
        public Matrix Projection { get; private set; }
        public Vector3 Position { get { return gameObj.Transform.Position; } }

        public FearCamera([Named("FirstPersonCameraObject")]GameObject gObj)
        {
            gameObj = gObj;
            gameObj.Transform.Changed += OnTransformChanged;

            Projection = Matrix.PerspectiveFovLH(SharpDX.MathUtil.Pi * 0.25f, 1280.0f/720.0f, 0.01f, 1000.0f);
            View = Matrix.LookAtLH(gameObj.Transform.Position, new Vector3(0, 0, 0), Vector3.UnitY);
        }

        public void AdjustProjection(float fov, float aspect, float near, float far)
        {
            Projection = Matrix.PerspectiveFovLH(SharpDX.MathUtil.Pi * 0.25f, 1280.0f/720.0f, 0.01f, 1000.0f);
        }

        void OnTransformChanged(Transform newTransform)
        {
            View = Matrix.LookAtLH(newTransform.Position, newTransform.Position + newTransform.Forward, Vector3.UnitY);
        }
    }
}
