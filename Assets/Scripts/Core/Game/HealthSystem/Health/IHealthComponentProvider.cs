using Game.Core.Health;

namespace Core.Game.HealthSystem.Health
{
    public interface IHealthComponentProvider
    {
        public IHealthComponent HealthComponent { get; }
    }
}