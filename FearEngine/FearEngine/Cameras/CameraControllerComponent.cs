using FearEngine.GameObjects;
using FearEngine.Inputs;
using FearEngine.Timer;
using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Input;

namespace FearEngine.Cameras
{
    //TODO The reason that we're getting popping when we first move the camera is because our YawPitchRoll starts at zero. Regardless of what the camera might be set at.
    public class CameraControllerComponent : Updateable
    {
        private const float STRAFE_SPEED = 0.025f;
        private const float WALK_SPEED = 0.025f;
        private const float ROTATION_SPEED = 0.5f;

        private float strafeDir;
        private float walkDir;
        private Vector2 rotationDir;

        private float yawSum;
        private float pitchSum;

        Input input;
        Transform transform;

        public CameraControllerComponent(Input inputParam)
        {
            input = inputParam;

            strafeDir = 0.0f;
            walkDir = 0.0f;
            rotationDir = Vector2.Zero;

            yawSum = 0.0f;
            pitchSum = 0.0f;
        }

        public void Update(GameObject owner, GameTimer gameTime)
        {
            transform = owner.Transform;

            CheckInput();

            if( NeedToUpdate())
            {
                if(strafeDir != 0.0f)
                {
                    ShiftInDirection(transform.Right * strafeDir, STRAFE_SPEED * gameTime.ElapsedGameTime.Milliseconds);
                }

                if(walkDir != 0.0f)
                {
                    ShiftInDirection(transform.Forward * walkDir, WALK_SPEED * gameTime.ElapsedGameTime.Milliseconds);
                }

                if(!rotationDir.Equals(Vector2.Zero))
                {
                    yawSum += rotationDir.X * ROTATION_SPEED * gameTime.ElapsedGameTime.Milliseconds;
                    pitchSum += rotationDir.Y * ROTATION_SPEED * gameTime.ElapsedGameTime.Milliseconds;

                    yawSum = SharpDX.MathUtil.Wrap(yawSum, -SharpDX.MathUtil.Pi, SharpDX.MathUtil.Pi);
                    pitchSum = SharpDX.MathUtil.Clamp(pitchSum, -SharpDX.MathUtil.PiOverFour, SharpDX.MathUtil.PiOverFour);
                }

                Quaternion yaw = Quaternion.RotationAxis(Vector3.Up, yawSum);
                Quaternion pitch = Quaternion.RotationAxis(Vector3.Right, pitchSum);
                transform.SetRotation(yaw * pitch);
            }
        }

        private bool NeedToUpdate()
        {
            return strafeDir != 0.0f || walkDir != 0.0f || !rotationDir.Equals(Vector2.Zero);
        }

        private void CheckInput()
        {
            strafeDir = 0.0f;
            if (input.IsKeyDown(Keys.D))
            {
                strafeDir += 1.0f;
            }

            if (input.IsKeyDown(Keys.A))
            {
                strafeDir += -1.0f;
            }

            walkDir = 0.0f;
            if(input.IsKeyDown(Keys.W))
            {
                walkDir += 1.0f;
            }

            if(input.IsKeyDown(Keys.S))
            {
                walkDir += -1.0f;
            }

            rotationDir = Vector2.Zero;
            if (input.IsMouseButtonDown(MouseButton.RightMouseButton))
            {
                rotationDir = input.MouseDelta;
            }
        }

        private void ShiftInDirection(Vector3 direction, float amount)
        {
            transform.MoveTo(transform.Position + (direction * amount));
        }
    }
}