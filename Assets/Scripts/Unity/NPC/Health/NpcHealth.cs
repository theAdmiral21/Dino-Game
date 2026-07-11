using Core.Game.HealthSystem.Damage;
using Core.Game.HealthSystem.Health;
using Game.Core.Execution;
using Game.Core.Health;
using Infrastructure.Unity.Registries;
using Movement.Core.Stats;
using Movement.Unity.Abstractions;
using NPC.Application.Health;
using Primitives.Damage;
using Primitives.Stats.DataStructures;
using Unity.Common;
using UnityEngine;

namespace NPC.Unity.Health
{
    public class NpcHealth : SelfRegister<IInitializable<IGameContext>>, IDamageable, IHealable, IHealthComponentProvider, IInitializable<IGameContext>, IHealProvider, IDamageProvider
    {
        [SerializeField] private int _priority = 0;
        public int Priority => _priority;
        public IHealthComponent HealthComponent { get; private set; }
        public IDamageable Damageable => this;
        public IHealable Healable => this;

        [Header("Debug")]
        [SerializeField] int _currentHealth;

        public void Initialize(IGameContext context)
        {
            IStatCollection statCollection = ProviderLookUp.Require<IStatProvider>(this).StatSheet.StatCollection;
            statCollection.TryGet<HealthStats>(out var healthStats);
            int maxHealth = (int)healthStats.TotalHealth.Value;

            HealthComponent = new NpcHealthComponent(maxHealth);
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

        private void LateUpdate()
        {
            _currentHealth = HealthComponent.CurrentHealth;
        }
    }
}