using FearEngine.GameObjects;
using FearEngine.Input;
using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Input;

namespace FearEngine
{
    class CameraControllerComponent : Updateable
    {
        private const float STRAFE_SPEED = 0.025f;
        private const float WALK_SPEED = 0.025f;
        private const float ROTATION_SPEED = 0.5f;

        private float StrafeDir;
        private float WalkDir;
        private Vector2 RotationDir;

        FearInput input;

        public CameraControllerComponent(FearInput inputParam)
        {
            input = inputParam;

            StrafeDir = 0.0f;
            WalkDir = 0.0f;
            RotationDir = Vector2.Zero;
        }


        public void Update(GameObject owner, GameTime gameTime)
        {
            UpdateInput();

            Transform parentTransform = owner.Transform;
            Strafe(parentTransform, gameTime);
            Walk(parentTransform, gameTime);
            Pitch(parentTransform, gameTime);
            Yaw(parentTransform, gameTime);
        }

        public void UpdateInput()
        {
            StrafeDir = 0.0f;
            if (input.IsKeyDown(Keys.D))
            {
                StrafeDir += 1.0f;
            }

            if (input.IsKeyDown(Keys.A))
            {
                StrafeDir += -1.0f;
            }

            WalkDir = 0.0f;
            if(input.IsKeyDown(Keys.W))
            {
                WalkDir += 1.0f;
            }

            if(input.IsKeyDown(Keys.S))
            {
                WalkDir += -1.0f;
            }

            RotationDir = Vector2.Zero;
            if (input.IsMouseButtonDown(MouseButton.RightMouseButton))
            {
                RotationDir = input.GetMouseDelta();
            }
        }

        public void Strafe(Transform transform, GameTime gameTime)
        {
            if (StrafeDir != 0.0f)
            {
                transform.MoveTo(transform.Position + (transform.Right * StrafeDir * STRAFE_SPEED * gameTime.ElapsedGameTime.Milliseconds));
            }
        }

        public void Walk(Transform transform, GameTime gameTime)
        {
            if (WalkDir != 0.0f)
            {
                transform.MoveTo(transform.Position + (transform.Forward * WalkDir * WALK_SPEED * gameTime.ElapsedGameTime.Milliseconds));
            }
        }

        public void Pitch(Transform transform, GameTime gameTime)
        {
            if (RotationDir.Y != 0.0f)
            {
                transform.Pitch(RotationDir.Y * ROTATION_SPEED * gameTime.ElapsedGameTime.Milliseconds);
            }
        }

        public void Yaw(Transform transform, GameTime gameTime)
        {
            if (RotationDir.X != 0.0f)
            {
                transform.Yaw(RotationDir.X * ROTATION_SPEED * gameTime.ElapsedGameTime.Milliseconds);
            }
        }
    }
}