using System.Collections.Generic;
using Core.Movement.Inputs;
using Game.Core.Effects;
using Gameplay.Common.Unity.VisualEffects;
using Movement.Core.Abstractions;
using Movement.Core.Rules;
using PlayerController.Core.Effects.Abstractions;
using PlayerController.Core.Effects.DataStructures;
using Primitives.Physics;
using UnityEngine;

namespace PlayerController.Unity.Animations
{
    public class AnimatorEffectBridge : MonoBehaviour, IPlayerAnimator
    {
        private static readonly int isFallingHash = Animator.StringToHash("fallTrigger");
        private static readonly int isRisingHash = Animator.StringToHash("risingTrigger");
        private static readonly int isJumpingHash = Animator.StringToHash("jumpTrigger");
        private static readonly int isRunningHash = Animator.StringToHash("isRunning");
        private static readonly int isGroundedHash = Animator.StringToHash("isGrounded");
        private static readonly int isWalkingHash = Animator.StringToHash("isWalking");
        private static readonly int isCrouchingHash = Animator.StringToHash("isCrouching");
        private static readonly int isDodgingHash = Animator.StringToHash("isDodging");
        private static readonly int dodgeBoolHash = Animator.StringToHash("dodgeBool");
        private HashSet<int> _availableParams;


        [SerializeField] private Transform _animatorTransform;
        [SerializeField] private Animator _animator;
        [SerializeField] private DamageFlash _damageFlash;
        [SerializeField] private IFramesAlpha _iFramesAlpha;
        private Vector3 _transformCache = Vector3.one;
        // public PlayerAnimatorState CurrentState => _currentState;
        // private PlayerAnimatorState _currentState;

        private bool _fallTriggered;
        private bool _jumpTriggered;
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
            SetBoolSafe(isGroundedHash, physicsContext.IsGrounded || physicsContext.IsOnPlatform);

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
                        // _animator.SetTrigger("isDodging");
                        SetTriggerSafe(isDodgingHash);
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
                        // _animator.SetBool("isCrouching", crouch.CrouchValue);
                        SetBoolSafe(isCrouchingHash, crouch.CrouchValue);
                        break;
                    }
                case JumpEffect jump:
                    {
                        // Debug.Log("Animating jump");

                        AnimateJump();
                        break;
                    }
                case LandEffect landing:
                    {
                        // Debug.Log("Animating landing");
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
                // _animator.SetTrigger("jumpTrigger");
                SetTriggerSafe(isJumpingHash);
                _jumpTriggered = true;
            }
        }

        // private void AnimateDodge(IRuleState ruleState, PhysicsContext physicsContext)
        // {
        //     if (!ruleState.TryGet<IDodgeState>(out var dodgeState)) return;


        //     if (dodgeState.IsDodging)
        //     {
        //         // _animator.SetTrigger("isDodging");
        //         SetTriggerSafe(isDodgingHash);
        //     }
        //     // else
        //     // {
        //     //     _animator.SetBool("isDoubleJumping", false);
        //     // }
        // }

        private void AnimateFall(PhysicsContext physicsContext)
        {
            if (physicsContext.IsFalling && !_animator.GetBool("isDoubleJumping"))
            {
                // _animator.SetTrigger("fallTrigger");
                SetTriggerSafe(isFallingHash);
                _fallTriggered = true;
                _jumpTriggered = false;
            }
            else
            {
                _animator.ResetTrigger(isFallingHash);
            }
        }
        private void AnimateRising(PhysicsContext physicsContext)
        {
            if (physicsContext.IsRising && !physicsContext.IsGrounded && !_jumpTriggered)
            {
                // _animator.SetTrigger("risingTrigger");
                SetTriggerSafe(isRisingHash);
                // _animator.SetBool("isRising", true);
                SetBoolSafe(isRisingHash, true);
                // _fallTriggered = true;
            }
            else
            {
                _animator.ResetTrigger("risingTrigger");
                SetBoolSafe(isRisingHash, false);
            }
        }

        private void AnimateWalk(PhysicsContext physicsContext, IActorInput inputValue)
        {
            if (inputValue.TryGet<IPlayerInputs>(out var playerInputs))
            {
                if ((physicsContext.IsGrounded || physicsContext.IsOnPlatform) && playerInputs.Move.x != 0 && !playerInputs.SprintPressed)
                {
                    // Debug.Log("Animating run");
                    // _animator.SetBool("isWalking", true);
                    SetBoolSafe(isWalkingHash, true);
                    // _animator.SetFloat("runSpeed", Mathf.Abs(playerInputs.Move.x) / 1f);
                }
                else
                {
                    // _animator.SetBool("isWalking", false);
                    SetBoolSafe(isWalkingHash, false);
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
                    // _animator.SetBool("isRunning", true);
                    SetBoolSafe(isRunningHash, true);
                }
                else
                {
                    // _animator.SetBool("isRunning", false);
                    SetBoolSafe(isRunningHash, false);
                }
            }
        }

        private void AnimateCrouch(IRuleState ruleState)
        {
            if (!ruleState.TryGet<ICrouchState>(out var crouch)) return;
            // Debug.Log("Animating run");
            // _animator.SetBool("isCrouching", crouch.IsCrouching);
            SetBoolSafe(isCrouchingHash, crouch.IsCrouching);

        }

        // private void AnimateCrouchWalk(PhysicsContext physicsContext, IActorInput inputValue)
        // {
        //     if (inputValue.TryGet<IPlayerInputs>(out var playerInputs))
        //     {
        //         if ((physicsContext.IsGrounded || physicsContext.IsOnPlatform) && playerInputs.Move.x != 0 && playerInputs.Move.y < 0)
        //         {
        //             // Debug.Log("Animating run");
        //             _animator.SetBool("isCrouching", true);
        //             // _animator.SetFloat("runSpeed", Mathf.Abs(playerInputs.Move.x) / 1f);
        //         }
        //         else
        //         {
        //             _animator.SetBool("isCrouching", false);
        //         }
        //     }
        // }
    }
}