using Movement.Core.Abstractions;
using Movement.Core.Inputs;
using Movement.Core.Stats;
using Primitives.Physics;
using Primitives.Stats.DataStructures;
using UnityEngine;

namespace NPC.Core.Rules
{
    public class BallRules : IRuleState,
                             IDisabledState,
                             IStunState,
                             IGravityState,
                             IGroundedState,
                             IFallState,
                             IDirectionState,
                             ILandingState,
                             IXInputState,
                             IJumpState
    {
        // Rule state
        public float Dt { get; private set; }
        // ====================================================================================

        // Gravity State
        public bool ApplyGravity => _applyGravity;
        private bool _applyGravity;
        public bool AffectedByGravity { get; private set; } = true;
        // ====================================================================================

        // Fall State
        public FallType FallType => _fallType;
        private FallType _fallType;
        // ====================================================================================

        // Direction State
        public float Dir => _dir;
        // Default to the right
        private float _dir = 1;
        // ====================================================================================

        // X Input State
        public bool XInputLocked => _blockXCounter > 0;
        private readonly float _blockXTime;
        private float _blockXCounter;
        // ====================================================================================

        // Landing State
        public bool LandingStopRequested { get; set; }
        // ====================================================================================

        // Disable State
        public bool IsDisabled => _isDisabled;
        private bool _isDisabled;

        // ====================================================================================

        // Grounded State
        public bool GroundedLastFrame => _groundedLastFrame;
        private bool _groundedLastFrame;

        public float UngroundedCounter => _ungroundedCounter;
        private float _ungroundedCounter;
        // ====================================================================================

        // Stun State
        public bool IsStunned => _stunCounter > 0;
        private float _stunCounter;
        // ====================================================================================

        // Jump State
        private float _totalAllowedJumps;
        public float RemainingJumps => _remainingJumps;
        private float _remainingJumps;

        public bool CanCoyoteJump => _coyoteCounter > 0;
        private readonly float _coyoteTimer;
        private float _coyoteCounter;

        public bool JumpBuffered => _jumpBufferCounter > 0;
        private readonly float _jumpBufferTime;
        private float _jumpBufferCounter;

        public bool WallJumpBuffered => _wallJumpCounter > 0;
        private float _wallJumpTime;
        private float _wallJumpCounter;


        public BallRules(IStatCollection stats)
        {
            _totalAllowedJumps = stats.Get<JumpStats>().TotalJumps.Value;

            _coyoteTimer = stats.Get<JumpStats>().CoyoteTime.Value;
            _coyoteCounter = 0;

            _jumpBufferTime = stats.Get<JumpStats>().JumpBufferTime.Value;
            _jumpBufferCounter = 0;

            _applyGravity = true;
            _fallType = FallType.None;

            _blockXTime = stats.Get<WallStats>().WallJumpApexTime.Value;
            _blockXCounter = 0;
            //wall jump
            _wallJumpTime = stats.Get<WallStats>().WallJumpBufferTime.Value;
            _wallJumpCounter = 0;
        }

        public void UpdateRules(IActorInput inputValues, PhysicsContext physicsContext, float dt)
        {
            // Update dt
            Dt = dt;
            // Set the direction the actor is facing
            SetDirection(inputValues, physicsContext);
            // Start or update Ungrounded timer
            StartUngroundedTimer(physicsContext);
            // Tick active timers
            TickTimers();
        }

        public void ResetRuleState()
        {
            _ungroundedCounter = 0;
        }

        public void SetApplyGravity(bool val) => _applyGravity = val;
        public void SetFallType(FallType type) => _fallType = type;

        public void SetDirection(IActorInput input, PhysicsContext physicsContext)
        {
            float velX = physicsContext.Velocity.x;
            float velSign = Mathf.Sign(velX);

            if (velX != 0)
            {
                if (velSign != 0f)
                {
                    _dir = velSign;
                }
            }
            // Debug.Log($"Set enemy direction to: {_dir}");
        }

        public void ForceDirection(bool left)
        {
            if (left)
            {
                _dir = -1;
            }
            else
            {
                _dir = 1;
            }
            // Debug.Log($"Set direction to: {_dir} with input: {left}");
            return;
        }

        public void StartBlockXTimer()
        {
            _blockXCounter = _blockXTime;
        }



        public void SetDisabled(bool val)
        {
            _isDisabled = val;
        }

        public void UpdateGroundedLastFrame(PhysicsContext physicsContext)
        {
            _groundedLastFrame = physicsContext.IsGrounded || physicsContext.IsOnPlatform;
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

        private void TickTimers()
        {
            if (_stunCounter > 0)
            {
                _stunCounter -= Dt;
            }
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
            if (_wallJumpCounter > 0)
            {
                _wallJumpCounter -= Dt;
            }
        }

        public void DecrementJumps()
        {
            _remainingJumps -= 1;
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

        public void StartJumpBufferTimer()
        {
            _jumpBufferCounter = _jumpBufferTime;
        }

        public void ResetBufferTimer()
        {
            _jumpBufferCounter = 0;
        }

        public void StartCoyoteTimer(PhysicsContext physicsContext)
        {
            _coyoteCounter = _coyoteTimer;
        }

        public void StartWallJumpBufferTimer()
        {
            _wallJumpCounter = _wallJumpTime;
        }

        public void ResetWallJumpBufferTimer()
        {
            _wallJumpCounter = 0;
        }
    }
}