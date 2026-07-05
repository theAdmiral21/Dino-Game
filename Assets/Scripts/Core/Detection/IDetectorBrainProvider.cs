using Core.Detection;

namespace Core.Detection
{
    public interface IDetectorBrainProvider
    {
        public IDetectorBrain DetectorBrain { get; }
    }
}