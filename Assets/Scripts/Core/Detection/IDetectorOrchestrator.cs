using Core.Ai.State.BehaviorContext;

namespace Core.Detection
{
    public interface IDetectorOrchestrator : IDetectorBrainProvider
    {
        // public IDetectorBrain DetectorBrain { get; }
        public void InitBrain(IPerceptionContext context);
    }
}