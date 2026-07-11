using Gameplay.Common.Core.DataStructures;
using Primitives.Damage;
using UnityEngine;
using Unity.Common.Unity;
using PlayerController.Application.Health;
using Game.Core.Execution;
using PlayerController.Core.ManagerControls.Abstractions;
using Game.Core.Health;
using PlayerController.Core.Info;
using Movement.Core.Abstractions;
using Core.Game.HealthSystem.Health;
using Primitives.Players;
using Infrastructure.Unity.Registries;
using Unity.Common;
using Unity.Infrastructure.Providers;
using Core.Game.HealthSystem.Damage;
using Movement.Core.Stats;
using Primitives.Stats.DataStructures;

namespace PlayerController.Unity.Health
{
    public class PlayerHealth : SelfRegister<IInitializable<IGameContext>>,
                                IDamageable,
                                IHealable,
                                IHealthComponentProvider,
                                IInitializable<IGameContext>,
                                IHealProvider,
                                IDamageProvider
    {
        [SerializeField] SerializedInterface<IPlayerView> _playerView;
        [SerializeField] SerializedInterface<IOverrideControls> _overrideControls;


        [SerializeField] private SerializedInterface<IKnockBackable> _knockBackMono;
        public IKnockBackable KnockBack => _knockBack;
        private IKnockBackable _knockBack => _knockBackMono.Interface;

        [SerializeField] private SerializedInterface<IStunnable> _stunMono;
        public IStunnable Stun => _stun;
        private IStunnable _stun => _stunMono.Interface;

        public IHealthComponent HealthComponent => _healthComponent;
        private IHealthComponent _healthComponent;
        public int CurrentHealth => _healthComponent.CurrentHealth;
        public bool IsAlive => _healthComponent.IsAlive;
        private IPlayerInfo _playerInfo;

        [Header("Debug")]
        [SerializeField] int _currentHealth;

        [Header("Init order")]
        [SerializeField] private int _priority = 5;
        public int Priority => _priority;

        public IDamageable Damageable => this;
        public IHealable Healable => this;

        private void Awake()
        {
            base.Awake();

            if (_knockBack == null)
            {
                Debug.LogError($"Unable to convert {_knockBackMono} to IKnockBackable.");
                return;
            }

            if (_stun == null)
            {
                Debug.LogError($"Unable to convert {_stunMono} to IStunnable.");
                return;
            }

        }

        public void Initialize(IGameContext context)
        {
            var provider = ProviderLookUp.Require<PlayerDataProvider>(this);
            _playerInfo = provider.PlayerInfo;
            IStatCollection statCollection = provider.StatSheet.StatCollection;
            statCollection.TryGet<HealthStats>(out var healthStats);
            int maxHealth = (int)healthStats.TotalHealth.Value;

            _healthComponent = new PlayerHealthComponent(maxHealth,
                                                context.EventBus,
                                                _playerInfo,
                                                _playerView.Interface,
                                                _overrideControls.Interface);

        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_healthComponent != null, "Health manager is null");
        }

        public void ReceiveDamage(DamageInfo damageInfo)
        {
            Debug.Log($"Taking damage; info: {damageInfo}");
            // Apply effects
            _knockBack.KnockBack(damageInfo.KnockBackApex, damageInfo.KnockBackVelocity);
            _stun.Stun(damageInfo.StunTime);
            _healthComponent.HandleDamage(damageInfo);
        }

        public void HandleHealthPickUp(HealthPickUpContext context)
        {
            Debug.Log($"Got health context");
            HealInfo info = new HealInfo(context.HealthAmount);
            Heal(info);
        }

        public void Heal(HealInfo info)
        {
            _healthComponent.HandleHealing(info);
        }

        private void LateUpdate()
        {
            _currentHealth = HealthComponent.CurrentHealth;
        }
    }
}