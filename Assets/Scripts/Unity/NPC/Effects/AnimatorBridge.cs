using System.Collections;
using Core.Movement.Inputs;
using Game.Core.Animations;
using Game.Core.Effects;
using Movement.Core.Abstractions;
using Movement.Core.Rules;
using NPC.Core.Effects;
using Primitives.Physics;
using UnityEngine;

namespace Enemy.Unity.Effects
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorBridge : MonoBehaviour, IAnimatorBridge
    {
        // [SerializeField] private GameObject _alertObject;
        [SerializeField] private Animator _animator;
        private Vector3 _transformCache = Vector3.one;
        private Transform _animatorTransform => transform;
        private bool _isAngry = false;
        private void Awake()
        {
            // _alertObject.SetActive(false);
        }

        public void ApplyEffect(IEffectResult effect)
        {
            switch (effect)
            {
                case AlertEffect alert:
                    {
                        // Debug.Log($"Play alert effect!");
                        _isAngry = true;
                        _animator.SetBool("IsAngry", _isAngry);
                        AnimateAlert();
                        break;
                    }
                case PassiveEffect alert:
                    {
                        // Debug.Log($"Play passive effect!");
                        _isAngry = false;
                        _animator.SetBool("IsAngry", _isAngry);
                        break;
                    }
                case DeathEffect death:
                    {
                        // Debug.Log($"Animating death effect!");
                        _isAngry = false;
                        AnimateDeath();
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
            throw new System.NotImplementedException();
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
            _animator.SetBool("IsAngry", false);
            _animator.SetBool("IsIdle", false);
            _animator.SetBool("IsWalking", false);
            _animator.SetBool("IsRunning", false);
            _animator.SetTrigger("IsDead");
        }

        private void AnimateIdle(PhysicsContext physicsContext, IActorInput inputValue)
        {
            // Debug.Log($"Animating idle");
            if ((physicsContext.IsGrounded || physicsContext.IsOnPlatform) && inputValue.Move.x == 0)
            {
                _animator.SetBool("IsIdle", true);
            }
            else
            {
                _animator.SetBool("IsIdle", false);
            }

        }

        private void AnimateMovement()
        {
            // Debug.Log($"Animate patrol");
            _animator.SetBool("IsIdle", false);
            _animator.SetBool("IsPatrol", true);
            _animator.SetBool("IsDead", false);
        }

        private void AnimateRun(PhysicsContext physicsContext, IActorInput inputValue)
        {
            if ((physicsContext.IsGrounded || physicsContext.IsOnPlatform) && inputValue.Move.x != 0 && _isAngry)
            {
                _animator.SetBool("IsRunning", true);
            }
            else
            {
                _animator.SetBool("IsRunning", false);
            }
        }

        private void AnimateWalk(PhysicsContext physicsContext, IActorInput inputValue)
        {
            if ((physicsContext.IsGrounded || physicsContext.IsOnPlatform) && inputValue.Move.x != 0 && !_isAngry)
            {
                _animator.SetBool("IsWalking", true);
            }
            else
            {
                _animator.SetBool("IsWalking", false);
            }
        }
    }
}