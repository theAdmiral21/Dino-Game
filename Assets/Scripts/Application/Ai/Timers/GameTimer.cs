using AI.Core.State.BehaviorContext;
using AI.Core.Timers;

namespace AI.Application.Timers
{
    public class GameTimer : IGameTimerContext
    {
        public ITimerContext Timer { get; private set; }

        public GameTimer(float waitTime)
        {
            Timer = new TimerContext(waitTime);
        }


    }
}