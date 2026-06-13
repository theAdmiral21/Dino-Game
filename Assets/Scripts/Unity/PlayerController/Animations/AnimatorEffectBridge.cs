using Game.Core.Effects;
using Gameplay.Common.Unity.VisualEffects;
using Movement.Core.Abstractions;
using Movement.Core.Inputs;
using Movement.Core.Rules;
using PlayerController.Application.Effects.Abstractions;
using PlayerController.Application.Effects.DataStructures;
using PlayerController.Core.Effects.Abstractions;
using PlayerController.Core.Effects.DataStructures;
using Primitives.Physics;
using UnityEngine;

namespace PlayerController.Unity.Animations
{
    public class AnimatorEffectBridge : MonoBehaviour, IPlayerAnimator
    {
        [SerializeField] private Transform _animatorTransform;
        [SerializeField] private Animator _animator;
        [SerializeField] private DamageFlash _damageFlash;
        [SerializeField] private IFramesAlpha _iFramesAlpha;
        private Vector3 _transformCache = Vector3.one;
        public PlayerAnimatorState CurrentState => _currentState;
        private PlayerAnimatorState _currentState;

        private bool _fallTriggered;
        private bool _jumpTriggered;

        public void SyncAnimation(IActorInput inputValue, PhysicsContext physicsContext, in IRuleState ruleState)
        {
            // Debug.Log($"input value: {inputValue.Move}");
            // Set the direction of the animator.
            ruleState.TryGet<IDirectionState>(out var dirState);
            _transformCache.x = dirState.Dir;
            _animatorTransform.localScale = _transformCache;

            AnimateCrouch(ruleState);
            // AnimateCrouchWalk(physicsContext, inputValue);
            AnimateWalk(physicsContext, inputValue);
            AnimateRun(physicsContext, inputValue);
            // AnimateDodge(ruleState, physicsContext);
            // Debug.Log($"Animator: Physics context is falling: {physicsContext.IsFalling}");
            AnimateFall(physicsContext);
            AnimateRising(physicsContext);

            // Evaluate the easy state stuff
            _animator.SetBool("isGrounded", physicsContext.IsGrounded || physicsContext.IsOnPlatform);

            // _animator.SetBool("isWallsliding", physicsContext.IsWallSliding);

            // evaluate the booleans to determine what is going on

        }

        public void ApplyEffect(IEffectResult effect)
        {
            switch (effect)
            {
                case DodgeEffect bark:
                    {
                        Debug.Log("Animating dodge");
                        _animator.SetTrigger("isDodging");
                        break;
                    }
                case HowlEffect howl:
                    {
                        // Debug.Log("Animating howl");
                        break;
                    }
                case CrouchEffect crouch:
                    {
                        Debug.Log($"Animating crouch value: {crouch.CrouchValue}");
                        _animator.SetBool("isCrouching", crouch.CrouchValue);
                        break;
                    }
                case JumpEffect jump:
                    {
                        // Debug.Log("Animating jump");

                        AnimateJump();
                        break;
                    }
                case DoubleJumpEffect jump:
                    {
                        // Debug.Log("Animating double jump");
                        _animator.SetTrigger("DoubleJump");
                        break;
                    }
                case WallJumpEffect wallJump:
                    {
                        // Debug.Log("Animating wall jump");
                        _animator.SetTrigger("wallJumpTrigger");
                        break;
                    }
                case LandEffect landing:
                    {
                        Debug.Log("Animating landing");
                        _fallTriggered = false;
                        _jumpTriggered = false;
                        break;
                    }
                case DamageEffect damage:
                    {
                        _damageFlash.UpdateFlash(damage);
                        break;
                    }
                case IFrameEffect iFrame:
                    {
                        _iFramesAlpha.UpdateAlpha(iFrame);
                        break;
                    }
            }
        }

        private void AnimateJump()
        {
            if (!_jumpTriggered)
            {
                _animator.SetTrigger("jumpTrigger");
                _jumpTriggered = true;
            }
        }

        private void AnimateDodge(IRuleState ruleState, PhysicsContext physicsContext)
        {
            if (!ruleState.TryGet<IDodgeState>(out var dodgeState)) return;


            if (dodgeState.IsDodging)
            {
                _animator.SetTrigger("isDodging");
            }
            // else
            // {
            //     _animator.SetBool("isDoubleJumping", false);
            // }
        }

        private void AnimateFall(PhysicsContext physicsContext)
        {
            if (physicsContext.IsFalling && !_animator.GetBool("isDoubleJumping"))
            {
                _animator.SetTrigger("fallTrigger");
                _fallTriggered = true;
                _jumpTriggered = false;
            }
            else
            {
                _animator.ResetTrigger("fallTrigger");
            }
        }
        private void AnimateRising(PhysicsContext physicsContext)
        {
            if (physicsContext.IsRising && !physicsContext.IsGrounded && !_jumpTriggered)
            {
                _animator.SetTrigger("risingTrigger");
                _animator.SetBool("isRising", true);
                // _fallTriggered = true;
            }
            else
            {
                _animator.ResetTrigger("risingTrigger");
                _animator.SetBool("isRising", false);
            }
        }

        private void AnimateWalk(PhysicsContext physicsContext, IActorInput inputValue)
        {
            if (inputValue.TryGet<IPlayerInputs>(out var playerInputs))
            {
                if ((physicsContext.IsGrounded || physicsContext.IsOnPlatform) && playerInputs.Move.x != 0 && !playerInputs.SprintPressed)
                {
                    // Debug.Log("Animating run");
                    _animator.SetBool("isWalking", true);
                    // _animator.SetFloat("runSpeed", Mathf.Abs(playerInputs.Move.x) / 1f);
                }
                else
                {
                    _animator.SetBool("isWalking", false);
                }
            }
        }

        private void AnimateRun(PhysicsContext physicsContext, IActorInput inputValue)
        {
            if (inputValue.TryGet<IPlayerInputs>(out var playerInputs))
            {
                if ((physicsContext.IsGrounded || physicsContext.IsOnPlatform) && playerInputs.Move.x != 0 && playerInputs.SprintPressed)
                {
                    // Debug.Log("Animating run");
                    _animator.SetBool("isRunning", true);
                }
                else
                {
                    _animator.SetBool("isRunning", false);
                }
            }
        }

        private void AnimateCrouch(IRuleState ruleState)
        {
            if (!ruleState.TryGet<ICrouchState>(out var crouch)) return;
            // Debug.Log("Animating run");
            _animator.SetBool("isCrouching", crouch.IsCrouching);

        }

        private void AnimateCrouchWalk(PhysicsContext physicsContext, IActorInput inputValue)
        {
            if (inputValue.TryGet<IPlayerInputs>(out var playerInputs))
            {
                if ((physicsContext.IsGrounded || physicsContext.IsOnPlatform) && playerInputs.Move.x != 0 && playerInputs.Move.y < 0)
                {
                    // Debug.Log("Animating run");
                    _animator.SetBool("isCrouching", true);
                    // _animator.SetFloat("runSpeed", Mathf.Abs(playerInputs.Move.x) / 1f);
                }
                else
                {
                    _animator.SetBool("isCrouching", false);
                }
            }
        }
    }
}