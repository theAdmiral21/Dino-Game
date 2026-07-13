using System;
using Primitives.Damage;
using Primitives.Health;

namespace Game.Core.Health
{
    public interface IHealthComponent
    {
        public int MaxHealth { get; }
        public int CurrentHealth { get; }
        public bool IsAlive { get; }
        public HealthState StateOfHealth { get; }
        public event Action OnDeath;
        public event Action OnHealed;
        public event Action OnDamaged;
        public void HandleHealing(HealInfo info);
        public void HandleDamage(DamageInfo damageInfo);
        public void SetHealth(int health);
    }
}