using Movement.Core.Movement.DataStructures;
using Primitives.Physics;
using Movement.Core.Enums;
using Primitives.Audio;
using Movement.Core.Rules;
using Movement.Core.Abstractions;
using Movement.Core.Inputs;

namespace Movement.Core.Movement
{
    /// <summary>
    /// This class is used to evaluate the following rules for when the player lands on a surface:
    /// - If there is no movement input, the player will stop.
    /// - If the surface they landed on is slippery, momentum will be conserved.
    /// 
    /// </summary>
    public static class LandingRules
    {
        public static LandingResult OnLand(PhysicsContext facts, object ruleState, ref IActorInput inputs)
        {
            // Verify the rule state can be evaluated
            if (!ruleState.TryGet<IGroundedState>(out var groundedState)) return Denied();
            if (!ruleState.TryGet<ILandingState>(out var landingState)) return Denied();

            if (!groundedState.GroundedLastFrame && (facts.IsGrounded || facts.IsOnPlatform))
            {
                // Debug.Log($"Landed on surface type: {facts.Surface}");
                landingState.LandingStopRequested = false;
                return Approved(facts);
            }
            return Denied();
        }

        private static LandingResult Approved(PhysicsContext physicsContext)
        {
            // Debug.Log("Landing approved");
            return new LandingResult(true, physicsContext.Surface, ActionPhase.Impulse);
        }
        private static LandingResult Denied()
        {
            // Debug.Log("Landing denied");
            return new LandingResult(false, SurfaceType.None, ActionPhase.Impulse);

        }
    }
}