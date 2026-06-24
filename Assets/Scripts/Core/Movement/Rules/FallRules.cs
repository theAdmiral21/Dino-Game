using Core.Movement.Inputs;
using Movement.Core.Abstractions;
using Movement.Core.Enums;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Rules;
using Primitives.Physics;


namespace Movement.Core.Movement
{
    /// <summary>
    /// This class is used to evaluate the following rules for falling:
    /// - The player is not grounded
    /// - The player is touching a wall and is not grounded
    /// - The player jumped is falling -> slow fall
    /// - If the player is wall jumping, do not override gravity
    /// 
    /// </summary>
    public static class FallRules
    {
        public static FallResult TryFall(PhysicsContext facts, IActorInput inputs, object ruleState)
        {
            // Verify the rule state can be evaluated
            // if (!ruleState.TryGet<IXInputState>(out var xInput)) return Denied();
            if (!ruleState.TryGet<IGravityState>(out var gravState)) return Denied();
            if (!ruleState.TryGet<IFallState>(out var fallState)) return Denied();

            // Do not override gravity during a wall jump
            // if (xInput.XInputLocked) return Denied();

            // Debug.Log($"Not Grounded: {!facts.IsGrounded}");
            // Debug.Log($"Not On Platform: {!facts.IsOnPlatform}");
            // Debug.Log($"Not Rising: {!facts.IsRising}");
            // Debug.Log($"Not WallSliding: {!facts.IsWallSliding}");
            bool isAirborne = !facts.IsGrounded && !facts.IsOnPlatform && !facts.IsRising;

            if (isAirborne)
            {
                gravState.SetApplyGravity(true);
                if (inputs.Move.y <= -0.5)
                {
                    fallState.SetFallType(FallType.Fast);
                    return Approved(FallType.Fast);
                }
                fallState.SetFallType(FallType.Slow);
                return Approved(FallType.Slow);
            }

            // // This essentially tells the fall calculator "stahp"
            // if (facts.IsGrounded || facts.IsOnPlatform)
            // {
            //     gravState.SetApplyGravity(false);
            //     fallState.SetFallType(FallType.None);
            //     return Approved(FallType.None);
            // }

            // if (facts.IsTouchingWall && !xInput.XInputLocked && isAirborne)
            // {
            //     if (PushingLeft(facts, inputs) || PushingRight(facts, inputs))
            //     {
            //         // Debug.Log($"Pushing Left: {PushingLeft(facts, inputs)}; Pushing Right: {PushingRight(facts, inputs)}");

            //         gravState.SetApplyGravity(false);
            //         fallState.SetFallType(FallType.WallSlide);
            //         return Approved(FallType.WallSlide);
            //     }
            //     fallState.SetFallType(FallType.Slow);
            //     return Approved(FallType.Slow);
            // }

            // NOTE Need a way to check input states here for slow and fast fall

            gravState.SetApplyGravity(false);
            fallState.SetFallType(FallType.None);
            return Denied();

        }

        private static bool PushingLeft(PhysicsContext facts, IActorInput inputs)
        {
            return facts.WallContactType == WallContact.Left && inputs.Move.x < -0.5f;
        }

        private static bool PushingRight(PhysicsContext facts, IActorInput inputs)
        {
            return facts.WallContactType == WallContact.Right && inputs.Move.x > 0.5f;
        }

        private static FallResult Approved(FallType type)
        {
            // Debug.Log($"Fall approved, type: {type}");
            return new FallResult
            (
                true,
                type,
                ActionPhase.Continuous
            );
        }
        private static FallResult Denied()
        {
            // Debug.Log("Fall denied");
            return new FallResult
            (
                false,
                FallType.None,
                ActionPhase.Continuous
            );
        }

    }
}