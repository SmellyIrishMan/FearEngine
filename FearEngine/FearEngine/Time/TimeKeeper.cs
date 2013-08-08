using System.Diagnostics;
namespace FearEngine.Time
{
    public static class TimeKeeper
    {
        private static int FrameCount { get; set; }
        private static Stopwatch Clock{ get; set;}
        private static double PreviousTime { get; set; }
        public static float Delta { get; private set; }

        public const int FIXED_TIME_STEP = 33;

        public static void Initialise()
        {
            FrameCount = 0;
            Clock = new Stopwatch();
            Clock.Start();
            PreviousTime = Clock.Elapsed.TotalMilliseconds;
        }

        public static void Update()
        {
            FrameCount++;

            double currentTime = Clock.Elapsed.TotalMilliseconds;
            long currentTick = Clock.Elapsed.Ticks;
            Delta = (float)(currentTime - PreviousTime);
            PreviousTime = currentTime;
        }
    }
}
