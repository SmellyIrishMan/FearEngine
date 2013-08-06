using FearEngine.Time;
using SharpDX;

namespace FearEngine
{
    class Transform
    {
        private const float STRAFE_SPEED = 5.0f;
        private const float WALK_SPEED = 5.0f;

        public Vector3 Position { get; set; }

        public Vector3 m_Forward { get; private set; }
        public Vector3 m_Right { get; private set; }
        public Vector3 m_Up { get; private set; }

        public void LookAt(Vector3 pos, Vector3 target, Vector3 worldUp)
        {
            Position = pos;

            m_Forward = Vector3.Normalize(target - pos);
            m_Right = Vector3.Normalize(Vector3.Cross(worldUp, m_Forward));
            m_Up = Vector3.Cross(m_Forward, m_Right);
        }

        public void Strafe(float dir)
        {
            Position = Position + (m_Right * dir * STRAFE_SPEED * TimeKeeper.Delta);
        }

        public void Walk(float dir)
        {
            Position = Position + (m_Forward * dir * WALK_SPEED * TimeKeeper.Delta);
        }

        public void Pitch(float angle)
        {
            Matrix rotation = Matrix.RotationAxis(m_Right, angle);
            m_Up = Vector3.TransformNormal(m_Up, rotation);
            m_Forward = Vector3.TransformNormal(m_Forward, rotation);
        }

        public void RotateY(float angle)
        {
            Matrix rotation = Matrix.RotationY(angle);

            m_Up = Vector3.TransformNormal(m_Up, rotation);
            m_Right = Vector3.TransformNormal(m_Right, rotation);
            m_Forward = Vector3.TransformNormal(m_Forward, rotation);
        }
    }
}