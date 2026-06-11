namespace Game.Core.Health
{
    public interface IHealthInfo
    {
        public bool IsAlive { get; }
        public int CurrentHealth { get; }
        public int MaxHealth { get; }
    }
}