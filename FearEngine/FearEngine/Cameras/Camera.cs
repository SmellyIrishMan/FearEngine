using FearEngine.GameObjects;
using FearEngine.Input;
using SharpDX;
using SharpDX.Toolkit;

namespace FearEngine.Cameras
{
    public class Camera
    {
        private GameObject gameObj;

        public Matrix View { get; private set; }
        public Matrix Projection { get; private set; }

        public Camera(GameObject gObj, float aspect)
        {
            gameObj = gObj;

            Projection = Matrix.PerspectiveFovLH(SharpDX.MathUtil.Pi * 0.25f, aspect, 0.01f, 1000.0f);
            View = Matrix.LookAtLH(gameObj.Transform.Position, new Vector3(0, 0, 0), Vector3.UnitY);

            gObj.Transform.Changed += OnTransformChanged;
        }

        void OnTransformChanged(Transform newTransform)
        {
            View = Matrix.LookAtLH(newTransform.Position, newTransform.Position + newTransform.Forward, Vector3.UnitY);
        }
    }
}
