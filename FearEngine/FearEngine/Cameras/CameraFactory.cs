using FearEngine.GameObjects;
using FearEngine.GameObjects.Updateables;
using SharpDX;

namespace FearEngine.Cameras
{
    public class CameraFactory
    {
        UpdateableFactory updateableFactory;

        public CameraFactory(UpdateableFactory updateableFac)
        {
            updateableFactory = updateableFac;
        }

        public FearCamera CreateDebugCamera(GameObject cameraObject)
        {
            cameraObject.AddUpdatable(updateableFactory.CreateCameraControllerComponent(cameraObject.Transform));

            Vector3 cameraPos = new Vector3(1, 5, -9);
            cameraObject.Transform.MoveTo(cameraPos);
            cameraObject.Transform.LookAt(Vector3.Zero);

            FearCamera camera = new FearCamera();
            camera.AdjustProjection(SharpDX.MathUtil.Pi * 0.25f, 1280.0f / 720.0f, 0.01f, 1000.0f);

            TransformAttacher transformAttacher = (TransformAttacher)camera;
            transformAttacher.AttactToTransform(cameraObject.Transform);

            return camera;
        }
    }
}
