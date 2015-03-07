using FearEngine.Input;
using SharpDX;
using SharpDX.Toolkit;

namespace FearEngine.Cameras
{
    public class Camera : GameObject
    {
        public Matrix View { get; private set;  }
        public Matrix Projection { get; private set; }

        readonly Updateable movementComponent;

        public Camera(string name, Transform transform, Updateable movement, float aspect)
            : base(name, transform)
        {
            movementComponent = movement;

            Projection = Matrix.PerspectiveFovLH(SharpDX.MathUtil.Pi * 0.25f, aspect, 0.01f, 1000.0f);
            View = Matrix.LookAtLH(Transform.Position, new Vector3(0, 0, 0), Vector3.UnitY);
        }

        public override void Update(GameTime gameTime)
        {
            movementComponent.Update(this, gameTime);
            View = Matrix.LookAtLH(Transform.Position, Transform.Position + Transform.Forward, Vector3.UnitY);
        }
    }
}
