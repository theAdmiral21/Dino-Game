using Core.Game.HealthSystem.Health;
using Game.Core.Execution;
using Game.Core.Health;
using Infrastructure.Unity.Registries;
using NPC.Application.Health;
using Primitives.Damage;
using UnityEngine;

namespace NPC.Unity.Health
{
    public class NpcHealth : SelfRegister<IInitializable<IGameContext>>, IDamageable, IHealable, IInitializable<IGameContext>, IHealthComponentProvider
    {
        public int MaxHealth = 1;
        public int Priority => 0;
        public IHealthComponent HealthComponent { get; private set; }
        public void Initialize(IGameContext context)
        {
            HealthComponent = new NpcHealthComponent(MaxHealth, context.EventBus);
        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(HealthComponent != null, "Health component is null");
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