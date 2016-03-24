using SharpDX;

namespace FearEngine.Cameras
{
    public interface Camera
    {
        Matrix View { get; }
        Matrix Projection { get; }
        Vector3 Position { get; }

        void AdjustProjection(float fov, float aspect, float near, float far);
    }
}
