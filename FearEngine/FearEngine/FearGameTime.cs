using SharpDX.Toolkit;
using System;

namespace FearEngine
{
    public class FearGameTime
    {
        GameTime sharpGameTime;

        public FearGameTime(GameTime gameTime)
        {
            sharpGameTime = gameTime;
        }

        public TimeSpan ElapsedGameTime
        {
            get
            {
                return sharpGameTime.ElapsedGameTime;
            }
        }

        public int FrameCount
        {
            get
            {
                return sharpGameTime.FrameCount;
            }
        }

        public bool IsRunningSlowly
        {
            get
            {
                return sharpGameTime.IsRunningSlowly;
            }
        }

        public TimeSpan TotalGameTime
        {
            get
            {
                return sharpGameTime.TotalGameTime;
            }
        }
    }
}
