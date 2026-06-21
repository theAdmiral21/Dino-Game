
using Movement.Core.Movement.DataStructures;
using Movement.Core.Inputs;
using Primitives.Physics;
using Movement.Core.Abstractions;

namespace Movement.Core.Rules
{
    public static class FrictionRules
    {
        public static FrictionResult TryFriction(PhysicsContext facts, object ruleState)
        {
            if (!ruleState.TryGet<IFrictionState>(out var frictionState)) Denied();

            if (facts.Velocity.x != 0 && (facts.IsGrounded || facts.IsOnPlatform))
            {
                return Approved();
            }
            return Denied();
        }

        private static FrictionResult Approved()
        {
            return new FrictionResult(true);
        }

        private static FrictionResult Denied()
        {
            return new FrictionResult(false);
        }

    }
}