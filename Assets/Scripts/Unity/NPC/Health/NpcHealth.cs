using Core.Game.HealthSystem.Health;
using Game.Core.Execution;
using Game.Core.Health;
using Infrastructure.Unity.Registries;
using NPC.Application.Health;
using Primitives.Damage;
using UnityEngine;

namespace NPC.Unity.Health
{
    public class NpcHealth : SelfRegister<IInitializable<IGameContext>>, IDamageable, IHealable, IHealthComponentProvider, IInitializable<IGameContext>
    {
        public int MaxHealth = 1;
        [SerializeField] private int _priority = 0;
        public int Priority => _priority;
        public IHealthComponent HealthComponent { get; private set; }

        public void Initialize(IGameContext context)
        {
            HealthComponent = new NpcHealthComponent(MaxHealth);
            Debug.Assert(HealthComponent != null, $"{gameObject.name} failed to construct HealthComponent");
        }

        public void PostInitialize(IGameContext context)
        {
        }

        public void Heal(HealInfo info)
        {
            HealthComponent.HandleHealing(info);
        }

        public void ReceiveDamage(DamageInfo damageInfo)
        {
            HealthComponent.HandleDamage(damageInfo);
        }
    }
}