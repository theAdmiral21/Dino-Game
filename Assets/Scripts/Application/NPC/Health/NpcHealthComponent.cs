using System;
using Game.Core.Health;
using Primitives.Damage;
using Primitives.EventBus.Abstractions;
using UnityEngine;

namespace NPC.Application.Health
{
    public class NpcHealthComponent : IHealthComponent
    {
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }
        public bool IsAlive => CurrentHealth > 0;
        public bool Respawning { get; private set; }
        public event Action OnDeath;
        public event Action OnHealed;
        public event Action OnDamaged;
        private IEventBus _eventBus;

        public NpcHealthComponent(int maxHealth, IEventBus eventBus)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            _eventBus = eventBus;
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
                    // case DamageType.Kill:
                    //     {
                    //         // Insta kill
                    //         Debug.Log($"Was killed - Frame: {Time.frameCount}");
                    //         DecrementHealth(CurrentHealth);
                    //         break;
                    //     }
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
                // _eventBus.Publish(new PlayerDiedEvent
                // {
                //     OverrideControls = _overrideControls,
                //     PlayerView = _playerView,
                // });
                OnDeath?.Invoke();
            }
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