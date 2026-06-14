using System;
using UnityEngine;
using Primitives.Damage;
using Primitives.EventBus.Abstractions;
using PlayerController.Core.Events;
using PlayerController.Core.ManagerControls.Abstractions;
using Game.Core.Health;
using PlayerController.Core.Info;

namespace PlayerController.Application.Health
{
    public class PlayerHealthComponent : IHealthComponent
    {
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }
        public bool IsAlive => CurrentHealth > 0;
        public bool Respawning { get; private set; }
        public event Action OnDeath;
        public event Action OnHealed;
        public event Action OnDamaged;
        private IEventBus _eventBus;
        private IPlayerView _playerView;
        private IOverrideControls _overrideControls;
        public PlayerHealthComponent(int maxHealth, IEventBus eventBus, IPlayerView playerView, IOverrideControls overrideControls)
        {
            if (maxHealth <= 0)
            {
                Debug.LogError($"Max health can not be less than or equal to zero.");
            }

            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            _eventBus = eventBus;
            _playerView = playerView;
            _overrideControls = overrideControls;
        }

        public PlayerHealthComponent(int maxHealth, int currentHealth, IEventBus eventBus, IPlayerView playerView, IOverrideControls overrideControls)
        {
            MaxHealth = maxHealth;
            CurrentHealth = currentHealth;
            _eventBus = eventBus;
            _playerView = playerView;
            _overrideControls = overrideControls;
        }
        public void HandleHealing(HealInfo info)
        {
            // You can only heal if you're alive
            if (IsAlive)
            {
                if (info.IsFullHeal)
                {
                    IncrementHealth(MaxHealth - CurrentHealth);
                }
                else
                {
                    IncrementHealth(info.HealAmount);
                }
            }
        }

        public void HandleDamage(DamageInfo damageInfo)
        {
            // Only take damage if you're alive
            if (IsAlive)
            {
                Debug.Log($"Is alive");
                switch (damageInfo.DamageType)
                {
                    case DamageType.Hurt:
                        {
                            Debug.Log($"Was hurt");
                            DecrementHealth(damageInfo.DamageValue);
                            break;
                        }
                    case DamageType.Stun:
                        {
                            // Insta kill
                            Debug.Log($"Was killed - Frame: {Time.frameCount}");
                            DecrementHealth(CurrentHealth);
                            break;
                        }
                    case DamageType.None:
                        {
                            // You can take hits, but you won't die
                            return;
                        }
                }
            }

            Debug.Log($"Current Health: {CurrentHealth}");
            if (!IsAlive)
            {
                Debug.Log($"Publishing death event");
                _eventBus.Publish(new PlayerDiedEvent
                {
                    OverrideControls = _overrideControls,
                    PlayerView = _playerView,
                });
                Respawning = true;
            }
        }

        private void DecrementHealth(int damage)
        {
            CurrentHealth -= damage;
            OnDamaged?.Invoke();
        }

        private void IncrementHealth(int healing)
        {
            if (CurrentHealth == MaxHealth) return;
            CurrentHealth += healing;
            OnHealed?.Invoke();
        }

        public void ResetHealth()
        {
            CurrentHealth = MaxHealth;
            OnHealed?.Invoke();
        }
    }
}