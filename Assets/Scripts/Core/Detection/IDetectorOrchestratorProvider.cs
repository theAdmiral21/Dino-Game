namespace Core.Detection
{
    public interface IDetectorOrchestratorProvider
    {
        public IDetectorOrchestrator DetectorOrchestrator { get; }
    }
}