using SharpDX;

namespace FearEngine.GameObjects.Updateables
{
    public interface UpdateableFactory
    {
        CameraControllerComponent CreateCameraControllerComponent(Transform trans);
        ContinuousRandomSlerp CreateContinuousRandomSlerp(float progressionPerSecond);
    }
}
