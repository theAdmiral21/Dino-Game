using Primitives.Detection;

namespace Core.Ai.State.BehaviorContext
{
    public interface IDetectorContext
    {
        public DetectorStats DetectionStats { get; }
    }
}