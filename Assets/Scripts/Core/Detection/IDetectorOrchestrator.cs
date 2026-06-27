using Core.Ai.State.BehaviorContext;

namespace Core.Detection
{
    public interface IDetectorOrchestrator
    {
        public IDetectorBrain Brain { get; }
        public void InitBrain(IPerceptionContext context);
    }
}