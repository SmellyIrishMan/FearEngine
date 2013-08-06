using System.Diagnostics;
namespace FearEngine.Time
{
    public static class TimeKeeper
    {
        public static Stopwatch Clock;

        public static float Delta
        {
            //TODO GIVE THIS SOME PROPER VALUES GOD DAMN IT!!!
            get { return 1.0f; }
        }
    }
}
