namespace Core.Detection.Visual
{
    public interface IVisible
    {
        public float Concealment { get; }
        public float Emission { get; }
        public float AmbientLight { get; }
        public float Velocity { get; }

        public void UpdateVisibility();
    }
}