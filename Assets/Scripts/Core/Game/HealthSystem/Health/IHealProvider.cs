using Game.Core.Health;

namespace Core.Game.HealthSystem.Health
{
    public interface IHealProvider
    {
        public IHealable Healable { get; }
    }
}