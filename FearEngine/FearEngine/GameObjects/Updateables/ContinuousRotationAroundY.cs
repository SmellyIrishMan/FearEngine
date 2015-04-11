using FearEngine.Timer;
using SharpDX;

namespace FearEngine.GameObjects.Updateables
{
    public class ContinuousRotationAroundY : Updateable
    {
        private float speed;

        public ContinuousRotationAroundY(float spd = 1.0f)
        {
            speed = spd;
        }

        public void Update(GameObject owner, GameTimer gameTime)
        {
            owner.Transform.Rotate(Quaternion.RotationAxis(Vector3.UnitY, (float)gameTime.ElapsedGameTime.TotalSeconds * speed));
        }
    }
}
