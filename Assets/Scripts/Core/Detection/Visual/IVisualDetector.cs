using Core.Detection.Visual.DataStructures;

namespace Core.Detection.Visual
{
    public interface IVisualDetector
    {
        public float VisualDistance { get; }
        public float Acuity { get; }
        public float NightVision { get; }
        public float AmbientLight { get; }
        public VisualData? Search();
        public VisualData? Look();
        public VisualData? Track();
    }
}