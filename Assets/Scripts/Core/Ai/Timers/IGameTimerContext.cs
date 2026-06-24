using AI.Core.State.BehaviorContext;

namespace AI.Core.Timers
{
    public interface IGameTimerContext
    {
        public ITimerContext Timer { get; }
    }
}