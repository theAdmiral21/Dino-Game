using Movement.Core.Movement.DataStructures;
using Primitives.Physics;
using UnityEngine;
using Movement.Core.Enums;
using Movement.Core.Rules;
using Movement.Core.Abstractions;
using Core.Movement.Inputs;

namespace Movement.Core.Movement
{
    /// <summary>
    /// This class is used to evaluate the following rules for a jump:
    /// - The player can only jump if they're grounded.
    /// - The player can only coyote jump if they were previously grounded.
    /// - The player can only double jump if they have an available jump.
    /// - A jump actions costs 1 jump
    /// 
    /// Coyote jump rules:
    /// - the player must NOT be grounded
    /// - the coyote jump timer must be > 0
    /// - the coyote jump timer starts when:
    ///     - the player was grounded last frame and is not grounded this frame
    ///     - the player's y velocity is < 0
    /// 
    /// </summary>
    public static class JumpRules
    {
        public static JumpResult TryJump(JumpRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            // Verify the rule state can be evaluated
            if (!ruleState.TryGet<IDirectionState>(out var direction)) return Denied(direction.Dir);
            if (!ruleState.TryGet<IDisabledState>(out var disabled)) return Denied(direction.Dir);
            if (!ruleState.TryGet<IStunState>(out var stunState)) return Denied(direction.Dir);
            if (!ruleState.TryGet<IJumpState>(out var jumpState)) return Denied(direction.Dir);
            if (!ruleState.TryGet<IXInputState>(out var xInputState)) return Denied(direction.Dir);
            if (!ruleState.TryGet<IGroundedState>(out var groundedState)) return Denied(direction.Dir);
            if (!ruleState.TryGet<ICrouchState>(out var crouch)) return Denied(direction.Dir);


            if (!request.Requested || disabled.IsDisabled || stunState.IsStunned) return Denied(direction.Dir);


            // Debug.Log($"Requested jump type: {request.JumpType}");


            if (facts.IsGrounded || facts.IsOnPlatform)
            {
                // Reset the buffer timer
                jumpState.ResetBufferTimer();
                // Eat a jump
                jumpState.DecrementJumps();


                // Otherwise do a regular jump
                return Approved(JumpType.Ground, 1, direction.Dir);
            }

            // Coyote Jump
            if (jumpState.CanCoyoteJump)
            {
                // Reset the buffer timer
                jumpState.ResetBufferTimer();
                // Eat a jump
                jumpState.DecrementJumps();

                // Otherwise do a regular jump
                return Approved(JumpType.Coyote, 1, direction.Dir);
            }


            return Denied(direction.Dir);
        }

        private static JumpResult Approved(JumpType type, int consumed, float dir)
        {
            // Debug.Log($"{type} approved");
            return new JumpResult
            (
                true,
                type,
                consumed,
                dir,
                ActionPhase.Impulse
            );
        }
        private static JumpResult Denied(float dir)
        {
            // Debug.Log($"Jump denied");
            return new JumpResult
            (
                false,
                JumpType.None,
                0,
                dir,
                ActionPhase.Impulse
            );
        }
    }
}