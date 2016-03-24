using FearEngine.Logger;
using System.Collections.Generic;
using System.Diagnostics;

namespace FearEngineTests.HelperClasses
{
    public class MethodTimer
    {
        private static Dictionary<string, Stopwatch> timers;

        public static void StartMethodTimer(string name)
        {
            EnsureTimersList();
            timers[name] = Stopwatch.StartNew();
        }

        public static void StopMethodTimerAndPrintResult(string name)
        {
            if (timers.ContainsKey(name))
            {
                timers[name].Stop();
                FearLog.Log("Time to execute " + name + ";\t" + timers[name].ElapsedMilliseconds + "ms");
            }
        }

        private static void EnsureTimersList()
        {
            if (timers == null)
            {
                timers = new Dictionary<string, Stopwatch>();
            }
        }
    }
}
