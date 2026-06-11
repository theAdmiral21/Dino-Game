using Movement.Core.Movement.DataStructures;
using Primitives.Physics;
using UnityEngine;
using Movement.Core.Enums;
using Movement.Core.Rules;
using Movement.Core.Abstractions;
using Movement.Core.Inputs;
using Movement.Core.DataStructures;
using NUnit.Framework;
using Primitives.Stats.DataStructures;

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
            // if (!ruleState.TryGet<IWallJumpState>(out var wallJumpState)) return Denied(direction.Dir);
            if (!ruleState.TryGet<IXInputState>(out var xInputState)) return Denied(direction.Dir);
            if (!ruleState.TryGet<IGroundedState>(out var groundedState)) return Denied(direction.Dir);
            // if (!ruleState.TryGet<IQuickStepState>(out var quickStepState)) return Denied(direction.Dir);
            // if (!ruleState.TryGet<ILongJumpState>(out var longJumpState)) return Denied(direction.Dir);
            // if (!ruleState.TryGet<ISwitchMovement>(out var switchMovement)) return Denied(direction.Dir);

            if (!request.Requested || disabled.IsDisabled || stunState.IsStunned) return Denied(direction.Dir);


            // request = ClassifyJump(request, jumpState, wallJumpState, facts);
            Debug.Log($"Requested jump type: {request.JumpType}");

            // bool canWallJumpNow =
            //     wallJumpState.WallJumpBuffered &&
            //     facts.IsTouchingWall &&
            //     !facts.IsGrounded;

            // if (canWallJumpNow)
            // {
            //     // Are wall jumps free? I think they are because you jump onto the wall and that eats a jump, but when you wall jump, you should be able to double jump after. So wall jumps are free

            //     // Reset the buffer timers
            //     jumpState.ResetBufferTimer();
            //     wallJumpState.ResetWallJumpBufferTimer();

            //     // Debug.Log("Called block x timer");
            //     xInputState.StartBlockXTimer();

            //     return Approved(JumpType.WallJump, 0, direction.Dir);
            // }

            if (facts.IsGrounded || facts.IsOnPlatform)
            {
                // Reset the buffer timer
                jumpState.ResetBufferTimer();
                // Eat a jump
                jumpState.DecrementJumps();

                // if (quickStepState.QuickStepActive)
                // {
                //     float elapsedTime = quickStepState.QuickStepTime - longJumpState.LongJumpCounter;
                //     if (elapsedTime < longJumpState.LongJumpFarWindow)
                //     {
                //         return Approved(JumpType.LongJumpFar, 1, direction.Dir);
                //     }
                //     return Approved(JumpType.LongJumpMed, 1, direction.Dir);
                // }

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

                // if (quickStepState.QuickStepActive)
                // {
                //     float elapsedTime = quickStepState.QuickStepTime - longJumpState.LongJumpCounter;
                //     if (elapsedTime < longJumpState.LongJumpFarWindow)
                //     {
                //         return Approved(JumpType.LongJumpFar, 1, direction.Dir);
                //     }
                //     return Approved(JumpType.LongJumpMed, 1, direction.Dir);
                // }

                // Otherwise do a regular jump
                return Approved(JumpType.Coyote, 1, direction.Dir);
            }

            //TODO Figure out if you want dashing or double jumping, default to double jump
#if UNITY_EDITOR
            // if (switchMovement.DashMode) return Denied(direction.Dir);
#endif

            // // Double Jump
            // if (jumpState.RemainingJumps > 0 &&
            //     groundedState.UngroundedCounter > .1f &&
            //     !jumpState.CanCoyoteJump &&
            //     !jumpState.JumpBuffered &&
            //     !wallJumpState.WallJumpBuffered
            //     )
            // {
            //     // Reset the buffer timer
            //     jumpState.ResetBufferTimer();
            //     // Eat two jump in case you were bounced upwards by something
            //     jumpState.DecrementJumps();
            //     jumpState.DecrementJumps();
            //     return Approved(JumpType.Double, 1, direction.Dir);
            // }

            return Denied(direction.Dir);
        }

        // private static JumpRequest ClassifyJump(JumpRequest jumpRequest, IJumpState jumpState, IWallJumpState wallState, PhysicsContext physicsContext)
        // {
        //     JumpContext jumpContext = jumpRequest.Context;

        //     float hitNormalDot = Vector2.Dot(jumpContext.FloorHitNormal, Vector2.up);
        //     float wallHitNormalDot = Mathf.Abs(Vector2.Dot(jumpContext.WallHitNormal, Vector2.right));

        //     // Debug.Log($"Jump context values. HitNormal: {jumpContext.FloorHitNormal}; MadeContact: {jumpContext.FloorMadeContact}; HitNormalDot: {hitNormalDot}");

        //     // Debug.Log($"Wall Jump context values. HitNormal: {jumpContext.WallHitNormal}; MadeContact: {jumpContext.WallMadeContact}; HitNormalDot: {wallHitNormalDot}");

        //     // Buffered jump
        //     if (jumpContext.FloorMadeContact && hitNormalDot > 0.7f)
        //     {
        //         // Start the buffer timer
        //         jumpState.StartJumpBufferTimer();
        //         jumpRequest.JumpType = JumpType.Ground;
        //         return jumpRequest;
        //     }
        //     // Buffered wall jump
        //     else if (jumpContext.WallMadeContact && wallHitNormalDot > 0.7f)
        //     {
        //         wallState.StartWallJumpBufferTimer();
        //         jumpRequest.JumpType = JumpType.WallJump;
        //         return jumpRequest;
        //     }
        //     // Wall jump while sliding
        //     else if (physicsContext.IsTouchingWall && !physicsContext.IsGrounded && !physicsContext.IsOnPlatform)
        //     {
        //         wallState.StartWallJumpBufferTimer();
        //         jumpRequest.JumpType = JumpType.WallJump;
        //         return jumpRequest;
        //     }
        //     jumpRequest.JumpType = JumpType.Double;
        //     return jumpRequest;

        // }

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