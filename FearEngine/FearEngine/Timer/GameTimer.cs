using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FearEngine.Timer
{
    public interface GameTimer
    {
        TimeSpan ElapsedGameTime { get; }
        TimeSpan TotalGameTime { get; }
    }
}
