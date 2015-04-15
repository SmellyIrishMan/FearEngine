using FearEngine.Logger;
using SharpDX;
using System;

namespace FearEngine.GameObjects.Updateables
{
    public class ContinuousRandomSlerp : Updateable
    {
        Quaternion targetRotation;
        float rateOfRotation;
        float progress;

        public ContinuousRandomSlerp(float progressionPerSecond)
        {
            rateOfRotation = progressionPerSecond;

            RandomiseTargetRotation();
        }

        private void RandomiseTargetRotation()
        {
            Random random = new Random();
            targetRotation = new Quaternion(GetRandomAngle(random), GetRandomAngle(random), GetRandomAngle(random), GetRandomAngle(random));
            targetRotation.Normalize();

            progress = 0.0f;
        }

        private static float GetRandomAngle(Random random)
        {
            return random.NextFloat(-MathUtil.Pi, MathUtil.Pi);
        }
        
        public void Update(GameObject owner, Timer.GameTimer gameTime)
        {
            progress += rateOfRotation * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Quaternion newRotation = Quaternion.Slerp(owner.Transform.Rotation, targetRotation, progress);
            owner.Transform.SetRotation(newRotation);

            if (progress >= 1.0f)
            {
                RandomiseTargetRotation();
            }
        }
    }
}
