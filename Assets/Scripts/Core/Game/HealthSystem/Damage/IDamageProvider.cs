using Primitives.Damage;

namespace Core.Game.HealthSystem.Damage
{
    public interface IDamageProvider
    {
        public IDamageable Damageable { get; }
    }
}