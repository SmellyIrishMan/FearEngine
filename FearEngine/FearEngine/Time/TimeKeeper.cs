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
            PreviousTime = Clock.ElapsedTicks;

            MSPerTick = (1.0 / Stopwatch.Frequency) / 1000.0;
        }

        public static void Update()
        {
            FrameCount++;

            long currentTime = Clock.ElapsedTicks;
            Delta = (float)((currentTime - PreviousTime) * MSPerTick);
            //Console.WriteLine("First half; " + (currentTime - PreviousTime));
            Console.WriteLine("Frames; " + FrameCount + "\tTime; " + Clock.ElapsedMilliseconds + "\tDelta; " + Delta);
            PreviousTime = currentTime;
        }
    }
}
