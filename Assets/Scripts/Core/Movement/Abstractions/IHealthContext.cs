using Game.Core.Health;

namespace Core.Movement.Abstractions
{
    public interface IHealthContext
    {
        public IHealthComponent HealthComponent { get; }
    }
}