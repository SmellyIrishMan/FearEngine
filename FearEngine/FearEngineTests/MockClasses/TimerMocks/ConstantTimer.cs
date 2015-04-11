using FearEngine.Timer;

namespace FearEngineTests.MockClasses.TimerMocks
{
    public class ConstantTimer : GameTimer
    {
        System.TimeSpan constantTimeSpan;
        System.TimeSpan totalTimeSpan;
        
        public ConstantTimer(System.TimeSpan constantTime)
        {
            constantTimeSpan = constantTime;
            totalTimeSpan = new System.TimeSpan(0);
        }

        public System.TimeSpan ElapsedGameTime
        {
            get 
            {
                totalTimeSpan += constantTimeSpan;
                return constantTimeSpan; 
            }
        }

        public System.TimeSpan TotalGameTime
        {
            get 
            { 
                return totalTimeSpan; 
            }
        }
    }
}
