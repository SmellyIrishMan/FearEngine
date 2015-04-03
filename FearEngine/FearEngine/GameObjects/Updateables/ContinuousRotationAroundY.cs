namespace FearEngine.GameObjects.Updateables
{
    public class ContinuousRotationAroundY : Updateable
    {
        private float speed = 0.1f;

        public ContinuousRotationAroundY(float spd)
        {
            speed = spd;
        }

        public void Update(GameObject owner, SharpDX.Toolkit.GameTime gameTime)
        {
            owner.Transform.Yaw((float)gameTime.TotalGameTime.TotalSeconds * speed);
        }
    }
}
