namespace Core.Detection
{
    public interface IDetectionManager
    {
        public IDetectionRegistry Detectors { get; }
    }
}