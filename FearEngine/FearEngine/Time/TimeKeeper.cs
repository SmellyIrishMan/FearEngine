using System.Diagnostics;
namespace FearEngine.Time
{
    public static class TimeKeeper
    {
        private static Stopwatch Clock{ get; set;}
        private static long PreviousTime{ get; set; }
        private static float m_delta;
        public static float Delta
        {
            get { return m_delta; }
        }

        public static void Initialise()
        {
            Clock = new Stopwatch();
            Clock.Start();
            PreviousTime = Clock.ElapsedMilliseconds;
        }

        public static void Update()
        {
            long currentTime = Clock.ElapsedMilliseconds;
            m_delta = (currentTime - PreviousTime) / 1000.0f;
            PreviousTime = currentTime;
        }
    }
}
