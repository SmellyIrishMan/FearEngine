using System;

namespace FearEngine.Timer
{
    public interface GameTimer
    {
        TimeSpan ElapsedGameTime { get; }
        TimeSpan TotalGameTime { get; }
    }
}
