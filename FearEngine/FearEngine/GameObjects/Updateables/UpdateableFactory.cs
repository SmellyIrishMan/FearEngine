using SharpDX;

namespace FearEngine.GameObjects.Updateables
{
    public interface UpdateableFactory
    {
        CameraControllerComponent CreateCameraControllerComponent();
        ContinuousRandomSlerp CreateContinuousRandomSlerp(float progressionPerSecond);
    }
}
