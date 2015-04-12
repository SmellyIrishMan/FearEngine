using FearEngine.Logger;
using SharpDX;
using System;

namespace FearEngine.GameObjects.Updateables
{
    public class OrbitTarget : Updateable
    {
        Vector3 target;
        Quaternion targetRotation;
        float rateOfRotation;
        float progress;

        float distanceBeforeRandomising = 0.001f;

        public OrbitTarget(Vector3 targ, float progressionPerSecond)
        {
            target = targ;
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
            return random.NextFloat(-MathUtil.Pi, -MathUtil.Pi);
        }
        
        public void Update(GameObject owner, Timer.GameTimer gameTime)
        {
            progress += rateOfRotation * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Quaternion newRotation = Quaternion.Slerp(owner.Transform.Rotation, targetRotation, progress);
            owner.Transform.SetRotation(newRotation);

            if (progress >= 1.0f)
            {
                FearLog.Log("Hit the target. Randomising");
                RandomiseTargetRotation();
            }
        }
    }
}
