namespace Core.Detection.Visual
{
    public interface IVisualDetector
    {
        public float Distance { get; }
        public float Acuity { get; }
        public float NightVision { get; }
        public float AmbientLight { get; }
        public float Look();
    }
}