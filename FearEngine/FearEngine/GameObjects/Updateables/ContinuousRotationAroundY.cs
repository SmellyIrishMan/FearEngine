using FearEngine.Timer;
using SharpDX;

namespace FearEngine.GameObjects.Updateables
{
    public class ContinuousRotationAroundY : Updateable
    {
        private Transform transform;
        private float speed;

        public ContinuousRotationAroundY(Transform trans, float spd = 1.0f)
        {
            speed = spd;
            transform = trans;
        }

        public void Update(GameTimer gameTime)
        {
            transform.Rotate(Quaternion.RotationAxis(Vector3.UnitY, (float)gameTime.ElapsedGameTime.TotalSeconds * speed));
        }
    }
}
