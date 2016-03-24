using SharpDX;
using System;

namespace FearEngine.GameObjects.Updateables
{
    public class ContinuousRandomSlerp : Updateable
    {
        Transform transform;
        Quaternion targetRotation;
        float rateOfRotation;
        float progress;

        public ContinuousRandomSlerp(Transform trans, float progressionPerSecond)
        {
            transform = trans;
            rateOfRotation = progressionPerSecond;
            progress = 1.0f;
            targetRotation = Quaternion.Identity;
        }

        private void RandomiseTargetRotation()
        {
            Random random = new Random();
            targetRotation = new Quaternion(0.0f, GetRandomAngle(random), GetRandomAngle(random), GetRandomAngle(random));
            targetRotation.Normalize();

            progress = 0.0f;
        }

        private static float GetRandomAngle(Random random)
        {
            return random.NextFloat(-MathUtil.PiOverFour, MathUtil.PiOverFour);
        }
        
        public void Update(Timer.GameTimer gameTime)
        {
            progress += rateOfRotation * (float)gameTime.ElapsedGameTime.TotalSeconds;

            Quaternion newRotation = Quaternion.Slerp(transform.Rotation, targetRotation, progress);
            transform.SetRotation(newRotation);
            if (progress >= 1.0f)
            {
                RandomiseTargetRotation();
            }
        }
    }
}
