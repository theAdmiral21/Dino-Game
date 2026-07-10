using System.Collections;
using System.Collections.Generic;
using Core.Movement.Inputs;
using Game.Core.Animations;
using Game.Core.Effects;
using Game.Core.Execution;
using Game.Core.Health;
using Infrastructure.Unity.Registries;
using Movement.Core.Abstractions;
using Movement.Core.Rules;
using NPC.Core.Effects;
using PlayerController.Core.Effects.DataStructures;
using Primitives.Physics;
using Unity.Common;
using Unity.Infrastructure.Providers;
using UnityEngine;

namespace Enemy.Unity.Effects
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorBridge : SelfRegister<IInitializable<IGameContext>>, IAnimatorBridge, IInitializable<IGameContext>
    {
        [SerializeField] private Animator _animator;

        private static readonly int IsAngryHash = Animator.StringToHash("IsAngry");
        private static readonly int IsIdleHash = Animator.StringToHash("IsIdle");
        private static readonly int IsWalkingHash = Animator.StringToHash("IsWalking");
        private static readonly int IsRunningHash = Animator.StringToHash("IsRunning");
        private static readonly int IsDeadHash = Animator.StringToHash("IsDead");
        private static readonly int DeadTrigger = Animator.StringToHash("DeadTrigger");
        private static readonly int LungeTrigger = Animator.StringToHash("LungeTrigger");
        private static readonly int BiteTrigger = Animator.StringToHash("BiteTrigger");
        private static readonly int isGroundedHash = Animator.StringToHash("IsGrounded");
        private static readonly int HurtTrigger = Animator.StringToHash("HurtTrigger");

        private HashSet<int> _availableParams;

        // [SerializeField] private SerializedInterface<IHealthComponentProvider> _healthComponentProviderMono;
        private IHealthComponent _healthComponent;

        private Vector3 _transformCache = Vector3.one;
        private Transform _animatorTransform => transform;

        [SerializeField] private int _priority = 1;
        public int Priority => _priority; // after the health component

        private bool _isAngry = false;
        private bool _isDead = false;
        private void Awake()
        {
            base.Awake();
            _availableParams = new();
            foreach (var param in _animator.parameters)
            {
                _availableParams.Add(param.nameHash);
            }
        }

        private void OnDestroy()
        {
            _healthComponent.OnDeath -= HandleDeath;
            _healthComponent.OnDamaged -= HandleHurt;
            base.OnDestroy();
        }

        private void SetBoolSafe(int hash, bool value)
        {
            if (_availableParams.Contains(hash))
            {
                _animator.SetBool(hash, value);
            }
        }

        private void SetTriggerSafe(int hash)
        {
            if (_availableParams.Contains(hash))
            {
                _animator.SetTrigger(hash);
            }
        }

        public void Initialize(IGameContext context)
        {
            // Get the health component
            var provider = ProviderLookUp.Require<RaptorDataProvider>(this);
            _healthComponent = provider.HealthComponent;
        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Log($"Subbing to death event");
            _healthComponent.OnDeath += HandleDeath;
            _healthComponent.OnDamaged += HandleHurt;
        }
        private void HandleDeath()
        {
            Debug.Log($"Handling death event");
            ApplyEffect(new DeathEffect());
        }

        private void HandleHurt()
        {
            Debug.Log($"Handling hurt event");
            ApplyEffect(new HurtEffect());
        }
        public void ApplyEffect(IEffectResult effect)
        {
            if (_isDead) return;
            switch (effect)
            {
                case AlertEffect alert:
                    {
                        // Debug.Log($"Play alert effect!");
                        _isAngry = true;
                        SetBoolSafe(IsAngryHash, _isAngry);
                        AnimateAlert();
                        break;
                    }
                case PassiveEffect alert:
                    {
                        // Debug.Log($"Play passive effect!");
                        _isAngry = false;
                        SetBoolSafe(IsAngryHash, _isAngry);
                        break;
                    }
                case DeathEffect death:
                    {
                        Debug.Log($"Animating death effect!");
                        _isAngry = false;
                        AnimateDeath();
                        break;
                    }
                case LungeEffect lunge:
                    {
                        AnimateAttack(lunge);
                        break;
                    }
                case LandEffect landing:
                    {
                        // Debug.Log("Animating landing");
                        // SetBoolSafe(IsLunging, false);
                        _animator.ResetTrigger(LungeTrigger);
                        break;
                    }
                case HurtEffect hurt:
                    {
                        // Debug.Log("Animating landing");
                        // SetBoolSafe(IsLunging, false);
                        SetTriggerSafe(HurtTrigger);
                        break;
                    }
                case BiteEffect bite:
                    {
                        Debug.Log($"Got bite effect");
                        AnimateAttack(bite);
                        break;
                    }
            }
        }

        public void SyncAnimation(IActorInput inputValues, PhysicsContext physicsContext, in IRuleState ruleState)
        {
            if (_isDead) return;
            SetDirection(ruleState);

            AnimateRun(physicsContext, inputValues);

            AnimateWalk(physicsContext, inputValues);

            AnimateIdle(physicsContext, inputValues);

            SetBoolSafe(isGroundedHash, physicsContext.IsGrounded || physicsContext.IsOnPlatform);
        }

        private void SetDirection(IRuleState ruleState)
        {
            // Set the direction of the animator.
            if (!ruleState.TryGet<IDirectionState>(out var dirState)) return;
            _transformCache.x = dirState.Dir;
            _animatorTransform.localScale = _transformCache;
        }
        private void AnimateAttack(IEffectResult effect)
        {
            // There could be a few different kind of attacks per dino. For now worry about the raptor
            switch (effect)
            {
                case LungeEffect lunge:
                    {
                        SetTriggerSafe(LungeTrigger);
                        break;
                    }
                case BiteEffect bite:
                    {
                        Debug.Log($"Set bite trigger");
                        SetTriggerSafe(BiteTrigger);
                        break;
                    }
            }


        }

        private void AnimateAlert()
        {
            // StartCoroutine(AlertRoutine(.75f));
        }
        private IEnumerator AlertRoutine(float alertTime)
        {
            // _alertObject.SetActive(true);
            yield return new WaitForSeconds(alertTime);
            // _alertObject.SetActive(false);
        }
        private void AnimateDeath()
        {
            SetTriggerSafe(DeadTrigger);
            _isDead = true;
            // Debug.Log($"Animate dead");
            // SetBoolSafe(IsAngryHash, false);
            // SetBoolSafe(IsIdleHash, false);
            // SetBoolSafe(IsWalkingHash, false);
            // SetBoolSafe(IsRunningHash, false);
            // SetBoolSafe(IsDeadHash, true);
        }

        private void AnimateIdle(PhysicsContext physicsContext, IActorInput inputValue)
        {
            // Debug.Log($"Animating idle");
            if ((physicsContext.IsGrounded || physicsContext.IsOnPlatform) && inputValue.Move.x == 0)
            {
                SetBoolSafe(IsIdleHash, true);
            }
            else
            {
                SetBoolSafe(IsIdleHash, false);
            }

        }

        private void AnimateRun(PhysicsContext physicsContext, IActorInput inputValue)
        {
            if ((physicsContext.IsGrounded || physicsContext.IsOnPlatform) && inputValue.Move.x != 0 && _isAngry)
            {
                SetBoolSafe(IsRunningHash, true);
            }
            else
            {
                SetBoolSafe(IsRunningHash, false);
            }
        }

        private void AnimateWalk(PhysicsContext physicsContext, IActorInput inputValue)
        {
            if ((physicsContext.IsGrounded || physicsContext.IsOnPlatform) && inputValue.Move.x != 0 && !_isAngry)
            {
                SetBoolSafe(IsWalkingHash, true);
            }
            else
            {
                SetBoolSafe(IsWalkingHash, false);
            }
        }

    }
}