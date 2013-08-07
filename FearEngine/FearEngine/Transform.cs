using System;
using System.Windows.Forms;
using FearEngine.Time;
using SharpDX;

namespace FearEngine
{
    public class Transform
    {
        private const float STRAFE_SPEED = 35.0f;
        private const float WALK_SPEED = 35.0f;

        public bool EnableFPSControl { get; set; }

        public Vector3 Position { get; set; }

        public Vector3 Forward { get; private set; }
        public Vector3 Right { get; private set; }
        public Vector3 Up { get; private set; }

        private bool IsStrafing { get; set; }
        private float StrafeDir { get; set; }
        private bool IsWalking { get; set; }
        private float WalkDir { get; set; }

        public Transform()
        {
            EnableFPSControl = true;
            InputManager.KeyDown += OnKeyDown;
            InputManager.KeyUp += OnKeyUp;
            InputManager.MouseMoved += OnMouseMoved;

            Position = Vector3.Zero;
            Forward = Vector3.UnitZ;
            Right = Vector3.UnitX;
            Up = Vector3.UnitY;
        }

        public void Update()
        {
            if (EnableFPSControl)
            {
                if (IsStrafing)
                {
                    Strafe(StrafeDir);
                }
                if (IsWalking)
                {
                    Walk(WalkDir);
                }
            }
        }

        private void OnMouseMoved(Vector2 delta)
        {
            if (EnableFPSControl)
            {
                Pitch(delta.Y);
                RotateY(delta.X);
            }
        }

        private void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if(EnableFPSControl)
            {
                switch(e.KeyCode)
                {
                    case Keys.D:
                        IsStrafing = true;
                        StrafeDir = 1.0f;
                        break;
                    case Keys.A:
                        IsStrafing = true;
                        StrafeDir = -1.0f;
                        break;
                    case Keys.W:
                        IsWalking = true;
                        WalkDir = 1.0f;
                        break;
                    case Keys.S:
                        IsWalking = true;
                        WalkDir = -1.0f;
                        break;
                }
            }
        }

        private void OnKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (EnableFPSControl)
            {
                switch (e.KeyCode)
                {
                    case Keys.D:
                    case Keys.A:
                        IsStrafing = false;
                        break;
                    case Keys.W:
                    case Keys.S:
                        IsWalking = false;
                        break;
                }
            }
        }

        public void LookAt(Vector3 pos, Vector3 target, Vector3 worldUp)
        {
            Position = pos;

            Forward = Vector3.Normalize(target - pos);
            Right = Vector3.Normalize(Vector3.Cross(worldUp, Forward));
            Up = Vector3.Cross(Forward, Right);
        }

        public void Strafe(float dir)
        {
            Position = Position + (Right * dir * STRAFE_SPEED * TimeKeeper.Delta);
        }

        public void Walk(float dir)
        {
            Position = Position + (Forward * dir * WALK_SPEED * TimeKeeper.Delta);
        }

        public void Pitch(float angle)
        {
            Matrix rotation = Matrix.RotationAxis(Right, angle * TimeKeeper.Delta);
            Up = Vector3.TransformNormal(Up, rotation);
            Forward = Vector3.TransformNormal(Forward, rotation);
        }

        public void RotateY(float angle)
        {
            Matrix rotation = Matrix.RotationY(angle * TimeKeeper.Delta);

            Up = Vector3.TransformNormal(Up, rotation);
            Right = Vector3.TransformNormal(Right, rotation);
            Forward = Vector3.TransformNormal(Forward, rotation);
        }
    }
}