using System.Collections;
using System.Collections.Generic;
using Core.Movement.Inputs;
using Game.Core.Animations;
using Game.Core.Effects;
using Movement.Core.Abstractions;
using Movement.Core.Rules;
using NPC.Core.Effects;
using NUnit.Framework;
using PlayerController.Core.Effects.DataStructures;
using Primitives.Physics;
using UnityEngine;

namespace Enemy.Unity.Effects
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorBridge : MonoBehaviour, IAnimatorBridge
    {
        [SerializeField] private Animator _animator;

        private static readonly int IsAngryHash = Animator.StringToHash("IsAngry");
        private static readonly int IsIdleHash = Animator.StringToHash("IsIdle");
        private static readonly int IsWalkingHash = Animator.StringToHash("IsWalking");
        private static readonly int IsRunningHash = Animator.StringToHash("IsRunning");
        private static readonly int IsDeadHash = Animator.StringToHash("IsDead");
        private static readonly int IsLunging = Animator.StringToHash("IsLunging");
        private static readonly int LungeTrigger = Animator.StringToHash("LungeTrigger");
        private static readonly int isGroundedHash = Animator.StringToHash("IsGrounded");

        private HashSet<int> _availableParams;

        private Vector3 _transformCache = Vector3.one;
        private Transform _animatorTransform => transform;
        private bool _isAngry = false;
        private bool _lungeTriggered = false;
        private void Awake()
        {
            _availableParams = new();
            foreach (var param in _animator.parameters)
            {
                _availableParams.Add(param.nameHash);
            }
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

        public void ApplyEffect(IEffectResult effect)
        {
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
                        // Debug.Log($"Animating death effect!");
                        _isAngry = false;
                        AnimateDeath();
                        break;
                    }
                case LungeEffect lunge:
                    {
                        AnimateAttack();
                        break;
                    }
                case LandEffect landing:
                    {
                        // Debug.Log("Animating landing");
                        // SetBoolSafe(IsLunging, false);
                        _animator.ResetTrigger(LungeTrigger);
                        break;
                    }
            }
        }

        public void SyncAnimation(IActorInput inputValues, PhysicsContext physicsContext, in IRuleState ruleState)
        {
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
        private void AnimateAttack()
        {
            // There could be a few different kind of attacks per dino. For now worry about the raptor
            // if (!_lungeTriggered)
            // {
            Debug.Log($"Animating lunge");
            // SetBoolSafe(IsLunging, true);
            SetTriggerSafe(LungeTrigger);
            // _lungeTriggered = true;
            // }

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
            Debug.Log($"Animate dead");
            SetBoolSafe(IsAngryHash, false);
            SetBoolSafe(IsIdleHash, false);
            SetBoolSafe(IsWalkingHash, false);
            SetBoolSafe(IsRunningHash, false);
            SetTriggerSafe(IsDeadHash);
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