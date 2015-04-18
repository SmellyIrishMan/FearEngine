using FearEngine.GameObjects;
using FearEngine.Inputs;
using FearEngine.Timer;
using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Input;

namespace FearEngine.GameObjects.Updateables
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

        private float yaw;
        private float pitch;

        Input input;
        Transform transform;

        public CameraControllerComponent(Transform trans, Input inputParam)
        {
            transform = trans;
            input = inputParam;

            strafeDir = 0.0f;
            walkDir = 0.0f;
            rotationDir = Vector2.Zero;

            yaw = 0.0f;
            pitch = 0.0f;
        }

        public void Update(GameTimer gameTime)
        {
            FearEngine.Logger.FearLog.Log("Updated camera component");
            CheckInput();

            if( NeedToUpdate())
            {
                FearEngine.Logger.FearLog.Log("Need to update");
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
                    yaw += rotationDir.X * ROTATION_SPEED * gameTime.ElapsedGameTime.Milliseconds;
                    pitch += rotationDir.Y * ROTATION_SPEED * gameTime.ElapsedGameTime.Milliseconds;

                    Rotate();
                }
            }
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
                FearEngine.Logger.FearLog.Log("Right button is down");
                rotationDir = input.MouseDelta;
            }
        }

        private bool NeedToUpdate()
        {
            return strafeDir != 0.0f || walkDir != 0.0f || !rotationDir.Equals(Vector2.Zero);
        }

        private void ShiftInDirection(Vector3 direction, float amount)
        {
            transform.MoveTo(transform.Position + (direction * amount));
        }

        private void Rotate()
        {
            FearEngine.Logger.FearLog.Log("Rotate");
            yaw = SharpDX.MathUtil.Wrap(yaw, -SharpDX.MathUtil.Pi, SharpDX.MathUtil.Pi);
            pitch = SharpDX.MathUtil.Clamp(pitch, -SharpDX.MathUtil.PiOverFour, SharpDX.MathUtil.PiOverFour);

            transform.SetRotation(Quaternion.RotationMatrix(Matrix.RotationYawPitchRoll(yaw, pitch, 0.0f)));
        }
    }
}