using System;
using System.Diagnostics;
namespace FearEngine.Time
{
    public static class TimeKeeper
    {
        private static int FrameCount { get; set; }
        private static Stopwatch Clock{ get; set;}
        private static long PreviousTime{ get; set; }
        private static double MSPerTick { get; set; }
        public static float Delta { get; private set; }

        public static void Initialise()
        {
            FrameCount = 0;
            Clock = new Stopwatch();
            Clock.Start();
            PreviousTime = Clock.ElapsedMilliseconds;
        }

        public static void Update()
        {
            FrameCount++;

            long currentTime = Clock.ElapsedMilliseconds;
            Delta = (float)(currentTime - PreviousTime);
            if(Delta > 0)
            {
                PreviousTime = currentTime;
            }
        }
    }
}
