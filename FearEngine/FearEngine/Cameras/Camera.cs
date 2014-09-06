using SharpDX;

namespace FearEngine.Cameras
{
    public class Camera : GameObject
    {
        public Matrix View { get; private set;  }
        public Matrix Projection { get; private set; }

        public Camera() : base()
        {
            Transform.Position = new Vector3(1, 2, -5);

            Projection = Matrix.PerspectiveFovLH(SharpDX.MathUtil.Pi * 0.25f, FearEngineApp.GetDevice().Viewport.AspectRatio, 0.01f, 1000.0f);
            View = Matrix.LookAtLH(Transform.Position, new Vector3(0, 0, 0), Vector3.UnitY);
        }

        public override void Update()
        {
            base.Update();
            //View = Matrix.LookAtLH(Transform.Position, Transform.Position + Transform.Forward, Vector3.UnitY);
        }

        public void UpdateView()
        {
            //transform.m_Forward = Vector3.Normalize(transform.m_Forward);
            //transform.m_Up = Vector3.Normalize(Vector3.Cross(transform.m_Forward, transform.m_Right));
            //transform.m_Right = Vector3.Cross(transform.m_Up, transform.m_Forward);

            //// Fill in the view matrix entries.
            //float x = -Vector3.Dot(transform.Position, transform.m_Right);
            //float y = -Vector3.Dot(transform.Position, transform.m_Up);
            //float z = -Vector3.Dot(transform.Position, transform.m_Forward);

            //View.M11 = transform.m_Right.X;
            //View.M21 = transform.m_Right.Y;
            //View.M31 = transform.m_Right.Z;
            //View.M41 = x;

            //View.M12 = transform.m_Up.X;
            //View.M22 = transform.m_Up.Y;
            //View.M32 = transform.m_Up.Z;
            //View.M42 = y;

            //View.M13 = transform.m_Forward.X;
            //View.M23 = transform.m_Forward.Y;
            //View.M33 = transform.m_Forward.Z;
            //View.M43 = z;

            //View.M14 = 0.0f;
            //View.M24 = 0.0f;
            //View.M34 = 0.0f;
            //View.M44 = 1.0f;
        }
    }
}
