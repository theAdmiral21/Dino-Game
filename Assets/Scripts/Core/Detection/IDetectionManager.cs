using Core.Detection.Olfactory;

namespace Core.Detection
{
    public interface IDetectionManager
    {
        // public IDetectionRegistry Detectors { get; }
        public IScentMap ScentMap { get; }
    }
}