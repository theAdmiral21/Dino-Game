using Movement.Core.Abstractions;
using Movement.Core.Enums;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Rules;
using Primitives.Physics;

namespace Movement.Core.Movement
{
    /// <summary>
    /// This class is used to evaluate the following rules for a jump cancel:
    /// - The player must be in the air
    /// - The player must not be holding the jump button
    /// - The player must not be wall sliding
    /// - The player can not jump cancel during a wall jump
    /// 
    /// </summary>
    public static class JumpCancelRules
    {
        public static JumpCancelResult TryJumpCancel(JumpCancelRequest request, PhysicsContext facts, object ruleState)
        {
            // Verify the rule state can be evaluated
            if (!ruleState.TryGet<IXInputState>(out var xInputState)) return Denied();
            if (!ruleState.TryGet<IGravityState>(out var gravityState)) return Denied();
            if (!ruleState.TryGet<IFallState>(out var fallState)) return Denied();

            if (!request.Requested) return Denied();

            // You must wait for the wall jump to complete before jumping
            if (xInputState.XInputLocked) return Denied();

            // if (!facts.IsGrounded && !facts.IsOnPlatform && !facts.IsWallSliding && facts.IsRising)
            if (!facts.IsGrounded && !facts.IsOnPlatform && fallState.FallType != FallType.WallSlide && facts.IsRising)
            {
                gravityState.SetApplyGravity(true);
                return Approved();
            }
            return Denied();
        }

        private static JumpCancelResult Approved()
        {
            // Debug.Log("Jump cancel approved");
            return new JumpCancelResult
            (
                true,
                ActionPhase.Impulse
            );
        }
        private static JumpCancelResult Denied()
        {
            // Debug.Log("Jump cancel denied");
            return new JumpCancelResult
            (
                 false,
                 ActionPhase.Impulse
            );
        }
    }
}