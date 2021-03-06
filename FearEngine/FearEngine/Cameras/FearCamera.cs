﻿using FearEngine.GameObjects;
using SharpDX;

namespace FearEngine.Cameras
{
    public class FearCamera : TransformAttacher, Camera
    {
        private Transform transform;

        public Matrix View { get; private set; }
        public Matrix Projection { get; private set; }
        public Vector3 Position { get { return transform.Position; } }

        public FearCamera()
        {
            transform = new Transform();
        }

        public void AdjustProjection(float fov, float aspect, float near, float far)
        {
            Projection = Matrix.PerspectiveFovLH(fov, aspect, near, far);
        }

        override sealed protected void OnTransformChanged(Transform newTransform)
        {
            View = Matrix.LookAtLH(newTransform.Position, newTransform.Position + newTransform.Forward, Vector3.UnitY);
        }
    }
}
