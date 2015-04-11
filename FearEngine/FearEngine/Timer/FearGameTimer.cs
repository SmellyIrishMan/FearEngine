using SharpDX.Toolkit;
using System;

namespace FearEngine.Timer
{
    class FearGameTimer : GameTimer
    {
        GameTime gameTime;

        public FearGameTimer(GameTime gameTimerProxy)
        {
            gameTime = gameTimerProxy;
        }

        public TimeSpan ElapsedGameTime
        {
            get { return gameTime.ElapsedGameTime; }
        }

        public TimeSpan TotalGameTime
        {
            get { return gameTime.TotalGameTime; }
        }
    }
}
