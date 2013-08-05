using SharpDX;

namespace FearEngine
{
    class Transform
    {
        public Vector3 Position{get; set;}

        public Vector3 m_Forward { get; private set; }
        public Vector3 m_Right { get; private set; }
        public Vector3 m_Up { get; private set; }

        public Quaternion m_Rotation;
    }
}
