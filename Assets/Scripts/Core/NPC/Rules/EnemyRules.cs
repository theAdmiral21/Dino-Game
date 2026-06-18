using Movement.Core.Abstractions;
using Movement.Core.Inputs;
using Primitives.Physics;
using UnityEngine;

namespace Enemy.Core.Rules
{
    public class EnemyRules : IRuleState,
                                IDisabledState,
                                IStunState,
                                IGravityState,
                                IGroundedState,
                                IDirectionState,
                                IFallState,
                                ILandingState,
                                IXInputState
    {
        public bool ApplyGravity => _applyGravity;
        private bool _applyGravity;
        public bool AffectedByGravity { get; private set; } = true;

        public FallType FallType => _fallType;
        private FallType _fallType;

        public bool GroundedLastFrame => _groundedLastFrame;
        private bool _groundedLastFrame;

        public float UngroundedCounter => _ungroundedCounter;
        private float _ungroundedCounter;

        public float Dir => _dir;
        private float _dir = 1; // Default to the right
        public bool LandingStopRequested { get; set; }

        public bool IsDisabled => _isDisabled;
        private bool _isDisabled;

        public float Dt { get; private set; }

        public bool XInputLocked => false;

        public bool IsStunned => false;

        public void UpdateRules(IActorInput inputValues, PhysicsContext physicsContext, float dt)
        {
            // Update dt
            Dt = dt;
            // Set the direction the actor is facing
            SetDirection(inputValues, physicsContext);
            // Start or update Ungrounded timer
            StartUngroundedTimer(physicsContext);
        }
        public void ResetRuleState()
        {
            _ungroundedCounter = 0;
        }

        public void SetApplyGravity(bool val) => _applyGravity = val;
        public void SetFallType(FallType type) => _fallType = type;
        public void SetDisabled(bool val) => _isDisabled = val;

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

        public void UpdateGroundedLastFrame(PhysicsContext physicsContext)
        {
            _groundedLastFrame = physicsContext.IsGrounded || physicsContext.IsOnPlatform;
        }



        public void StartBlockXTimer()
        {

        }

        public void StartStunnedTimer(float duration)
        {

        }
    }
}