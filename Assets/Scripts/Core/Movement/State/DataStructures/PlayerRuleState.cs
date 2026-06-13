using UnityEngine;
using Primitives.Physics;
using Movement.Core.Abstractions;
using Movement.Core.Inputs;
using Movement.Core.Stats;
using Primitives.Stats.DataStructures;
using Primitives.Input;
using System;
using Movement.Core.Rules;

namespace Movement.Core.State.DataStructures
{
    [System.Serializable]
    public class PlayerRuleState : IRuleState,
                                    IJumpState,
                                    IXInputState,
                                    IGroundedState,
                                    IDirectionState,
                                    IFallState,
                                    IGravityState,
                                    IStunState,
                                    IDisabledState,
                                    ILandingState,
                                    IDodgeState,
                                    IInvincibleState,
                                    ICrouchState
    {
        public float RemainingJumps => _remainingJumps;
        private float _remainingJumps;
        private float _totalAllowedJumps;

        public bool IsDisabled => _isDisabled;
        private bool _isDisabled;

        public bool ApplyGravity => _applyGravity;
        private bool _applyGravity;

        public bool AffectedByGravity => _affectedByGravity;
        private bool _affectedByGravity;

        public FallType FallType => _fallType;
        private FallType _fallType;

        public float Dt => _dt;
        private float _dt;

        public float Dir => _dir;
        private float _dir;

        public bool LandingStopRequested { get; set; }
        public bool GroundedLastFrame => _groundedLastFrame;
        private bool _groundedLastFrame;
        public bool IsInvincible => _isInvincible;
        private bool _isInvincible;


        /*
        =======================================================================================
        |                               Timer fields/props                                    |
        =======================================================================================
        */

        // Coyote jumping
        public bool CanCoyoteJump => _coyoteCounter > 0;
        private readonly float _coyoteTimer;
        private float _coyoteCounter;

        // X input lock - use primarily for wall jumping and quick stepping
        public bool XInputLocked => _blockXCounter > 0;
        private readonly float _blockXTime;
        private float _blockXCounter;

        // Buffered jump
        public bool JumpBuffered => _jumpBufferCounter > 0;
        private readonly float _jumpBufferTime;
        private float _jumpBufferCounter;

        // Ungrounded timer
        public float UngroundedCounter => _ungroundedCounter;
        private float _ungroundedCounter;
        public readonly float JumpApexTime;

        // Jump cool down timer
        public bool JumpCoolDownActive => _jumpCoolDownCounter > 0;
        private readonly float _jumpCoolDownTime;
        private float _jumpCoolDownCounter;

        // Stun timer
        public bool IsStunned => _stunCounter > 0;
        private float _stunCounter;
        private bool _stunnedLastFrame;

        // Invincibility timer
        private readonly float _iFrameTimer;
        private float _iFrameCounter;

        // Teleport cooldown timer
        public bool CanTeleport => _teleportCoolDownCounter <= 0;

        // Quick step values
        public float QuickStepDir { get; private set; }
        public float QuickStepTime { get; private set; }
        public float QuickStepCounter { get; private set; }

        public float QuickStepCoolDown { get; private set; }
        private float _quickStepCoolDownCounter;

        public bool QuickStepReady => _quickStepCoolDownCounter <= 0;

        public bool QuickStepActive => QuickStepCounter > 0 && !LongJumpIsActive;
        public bool QuickSteppingLastFrame => _quickSteppingLastFrame;

        // Long jump
        public bool CanLongJump => _longJumpCounter > 0;

        public bool LongJumpIsActive { get; private set; }
        public float LongJumpFarWindow { get; private set; }

        public float LongJumpMedWindow { get; private set; }
        public float LongJumpCounter => _longJumpCounter;

        public bool WallJumpBuffered => _wallJumpCounter > 0;

        // Zoomies
        public bool IsZooming { get; private set; }
        public float ZoomyAmount { get; private set; }
        public float MinimumRequiredZoom { get; private set; }
        public float ZoomLimit { get; private set; }

        // // Dashing
        // public bool IsDashing => DashCounter > 0;
        // public int DashAmount { get; private set; }
        // public float DashTime { get; private set; }
        // public float DashCounter { get; private set; }
        // public InputDirection DashDirection { get; private set; }
        // private int _totalDashes;

        // Debug movement type switch
        public bool DashMode { get; private set; }

        // Dodging
        public bool IsDodging => DodgeCounter > 0;
        public int DodgeAmount { get; private set; }
        public float DodgeTime { get; private set; }
        public float DodgeCounter { get; private set; }

        public InputDirection DodgeDirection { get; private set; }

        public bool IsCrouching { get; private set; }

        private int _totalDodges;

        private float _wallJumpTime;
        private float _wallJumpCounter;

        private float _longJumpCounter;
        private bool _quickSteppingLastFrame;

        private readonly float _teleportCoolDownTimer;
        private float _teleportCoolDownCounter;

        public PlayerRuleState(IStatCollection stats)
        {
            _totalAllowedJumps = stats.Get<JumpStats>().TotalJumps.Value;
            _remainingJumps = _totalAllowedJumps;
            JumpApexTime = stats.Get<JumpStats>().JumpApexTime.Value;

            _coyoteTimer = stats.Get<JumpStats>().CoyoteTime.Value;
            _coyoteCounter = 0;

            _jumpBufferTime = stats.Get<JumpStats>().JumpBufferTime.Value;
            _jumpBufferCounter = 0;

            _stunnedLastFrame = false;
            // IsDisabled = false;
            _applyGravity = true;
            _affectedByGravity = true;
            _fallType = FallType.None;


            _dt = 0;
            _dir = 1;

            _totalDodges = (int)stats.Get<DodgeStats>().TotalDodges.Value;
            DodgeTime = stats.Get<DodgeStats>().DodgeTime.Value;

        }

        public void ResetRuleState()
        {
            _coyoteCounter = 0;
            _blockXCounter = 0;
            _jumpBufferCounter = 0;
            _ungroundedCounter = 0;
            _jumpCoolDownCounter = 0;
            _stunnedLastFrame = false;
            _iFrameCounter = 0;
            _teleportCoolDownCounter = 0;
        }
        public void UpdateRules(IActorInput inputValues, PhysicsContext physicsContext, float dt)
        {
            // Update dt
            _dt = dt;
            // Set the direction the player is facing
            SetDirection(inputValues, physicsContext);
            // Reset the jumps
            ResetJumps(physicsContext);
            // Reset the dash
            ResetDodge(physicsContext);
            // Tick active timers
            TickTimers();

            // I think it is better to start timers after they've updated so that they don't exit prematurely because they're being updated before they're being evaluated.

            // Start the coyote timer
            StartCoyoteTimer(physicsContext);
            // Start or update Ungrounded timer
            StartUngroundedTimer(physicsContext);

            UpdateDodgeInvincibility();

            StartIFrameTimer();

            // StartQuickStepCoolDown();

            // StartLongJumpTimer();

            // UpdateLongJumpState();

            _stunnedLastFrame = IsStunned;
            // _quickSteppingLastFrame = QuickStepActive;

            UpdateCrouchState(inputValues);

        }

        public void StartBlockXTimer()
        {
            _blockXCounter = _blockXTime;
            // Debug.Log("Started BlockXTimer");
        }

        public void StartCoyoteTimer(PhysicsContext physicsContext)
        {
            // Start coyote timer
            // Debug.Log($"GroundedLastFrame: {GroundedLastFrame} Not Grounded: {!physicsContext.IsGrounded} IsFalling: {physicsContext.IsFalling} IsRising: {physicsContext.IsRising} IsFloating: {physicsContext.IsFloating}");

            // For some reason my fall trigger is a little slow, but the rising trigger is sensitive enough to prevent jumping from triggering this.
            if (GroundedLastFrame && !physicsContext.IsGrounded && !physicsContext.IsRising && !physicsContext.IsOnPlatform)
            {
                _coyoteCounter = _coyoteTimer;
                // Debug.Log("Started coyote timer");
            }
        }

        public void StartJumpBufferTimer()
        {

            _jumpBufferCounter = _jumpBufferTime;
            // Debug.Log("Started jump buffer timer");
        }
        public void UpdateDodgeInvincibility()
        {
            _isInvincible = IsDodging;
            // Debug.Log($"Player is invincible: {IsInvincible}");
        }
        public void StartIFrameTimer()
        {
            if (_stunnedLastFrame && !IsStunned && _iFrameCounter <= 0)
                _iFrameCounter = _iFrameTimer;
        }

        public void StartTeleportCoolDown()
        {
            _teleportCoolDownCounter = _teleportCoolDownTimer;
        }

        public void ResetBufferTimer()
        {
            // Debug.Log($"Reset buffer timer - Frame: {Time.frameCount}");
            _jumpBufferCounter = 0;
        }

        public void StartUngroundedTimer(PhysicsContext physicsContext)
        {
            if (!physicsContext.IsGrounded && !physicsContext.IsOnPlatform)
            {
                _ungroundedCounter += Dt;
            }
            else
            {
                _ungroundedCounter = 0;
            }
        }
        public void StartStunnedTimer(float duration)
        {
            _stunCounter = duration;
        }
        public void SetDirection(float dir)
        {
            _dir = dir;
        }
        public void SetDirection(IActorInput input, PhysicsContext physicsContext)
        {
            // float inputSign = Mathf.Sign(input.Move.x);

            float velX = physicsContext.Velocity.x;
            float velSign = Mathf.Sign(velX);

            if (velX != 0)
            {
                if (velSign != 0f)
                {
                    _dir = velSign;
                    // Debug.Log($"_dir = {_dir}");

                }
            }
        }
        public void ResetJumps(PhysicsContext physicsContext)
        {
            if ((physicsContext.IsGrounded || physicsContext.IsOnPlatform) && !GroundedLastFrame)
            {
                // Give the player their jumps back when they hit the ground
                _remainingJumps = _totalAllowedJumps;
                // Reset the buffer timer so it doesn't keep firing after the player lands.
                ResetBufferTimer();
            }
        }

        public void UpdateGroundedLastFrame(PhysicsContext physicsContext)
        {
            _groundedLastFrame = physicsContext.IsGrounded || physicsContext.IsOnPlatform;
        }

        public void SetApplyGravity(bool val) => _applyGravity = val;
        public void SetFallType(FallType type) => _fallType = type;
        public void DecrementJumps() => _remainingJumps -= 1;
        public void SetDisabled(bool val) => _isDisabled = val;
        private void TickTimers()
        {
            // Tick your timers with this dt
            if (_blockXCounter > 0)
            {
                _blockXCounter -= Dt;
            }
            if (_coyoteCounter > 0)
            {
                _coyoteCounter -= Dt;
            }
            if (_jumpBufferCounter > 0)
            {
                _jumpBufferCounter -= Dt;
            }
            if (_stunCounter > 0)
            {
                _stunCounter -= Dt;
            }
            if (_iFrameCounter > 0)
            {
                _iFrameCounter -= Dt;
            }
            if (_teleportCoolDownCounter > 0)
            {
                _teleportCoolDownCounter -= Dt;
            }
            if (QuickStepCounter > 0)
            {
                QuickStepCounter -= Dt;
            }
            if (_quickStepCoolDownCounter > 0)
            {
                _quickStepCoolDownCounter -= Dt;
            }
            if (_longJumpCounter > 0)
            {
                _longJumpCounter -= Dt;
            }
            if (_wallJumpCounter > 0)
            {
                _wallJumpCounter -= Dt;
            }
            if (IsZooming)
            {
                if (ZoomyAmount > 0)
                {
                    ZoomyAmount -= Dt;
                }
                else
                {
                    IsZooming = false;
                    ZoomyAmount = 0;
                }
            }
            if (DodgeCounter > 0)
            {
                DodgeCounter -= Dt;
            }
        }

        public void StartQuickStepTimer()
        {
            QuickStepCounter = QuickStepTime;
        }
        public void StartQuickStepCoolDown()
        {
            if (QuickSteppingLastFrame && !QuickStepActive)
            {
                _quickStepCoolDownCounter = QuickStepCoolDown;

            }
        }
        public void UpdateQuickSteppingLastFrame()
        {
            _quickSteppingLastFrame = QuickStepActive;
        }
        public void SetQuickStepDirection(float dir)
        {
            // You can NOT change directions mid dash
            if (QuickStepActive && !QuickSteppingLastFrame)
            {
                QuickStepDir = dir;
                return;
            }

            if (!QuickStepActive && QuickSteppingLastFrame)
            {
                QuickStepDir = 0;
                return;
            }
        }

        public void StartLongJumpTimer()
        {
            if (QuickStepActive && !CanLongJump)
            {
                _longJumpCounter = QuickStepTime;
            }
        }

        public void UpdateLongJumpState()
        {
            if (CanLongJump && !GroundedLastFrame)
            {
                LongJumpIsActive = true;
            }
            else
            {
                LongJumpIsActive = false;
            }
        }

        public void StartWallJumpBufferTimer()
        {
            _wallJumpCounter = _wallJumpTime;
        }

        public void ResetWallJumpBufferTimer()
        {
            _wallJumpCounter = 0;
        }

        public void AddZoomies(float zoomAmount)
        {
            // I should put some sort of limit on this.. right?
            ZoomyAmount += zoomAmount;

            if (ZoomyAmount > ZoomLimit)
            {
                ZoomyAmount = ZoomLimit;
            }
        }

        public void StartZoomiesTimer()
        {
            IsZooming = true;
        }

        public void DecrementDodge()
        {
            DodgeAmount -= 1;
        }

        public void ResetDodge(PhysicsContext physicsContext)
        {
            if ((physicsContext.IsGrounded || physicsContext.IsOnPlatform) && !IsDodging)
            {
                // Give the player their dash back when they hit the ground
                DodgeAmount = _totalDodges;
            }
        }

        public void StartDodgeTimer()
        {
            DodgeCounter = DodgeTime;
        }

        public void SetDodgeDirection(InputDirection direction)
        {
            DodgeDirection = direction;
        }

        public void SetCrouchState(bool val)
        {
            IsCrouching = val;
        }

        public void UpdateCrouchState(IActorInput inputValues)
        {
            inputValues.TryGet<IPlayerInputs>(out var playerInputs);

            if (IsDodging) SetCrouchState(false);

            if (FallType == FallType.Fast || FallType == FallType.Slow)
            {
                SetCrouchState(false);
            }

            if (playerInputs.JumpPressed) SetCrouchState(false);

            if (playerInputs.SprintPressed) SetCrouchState(false);
        }
    }
}