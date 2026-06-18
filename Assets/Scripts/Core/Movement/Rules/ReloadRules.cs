
using Movement.Core.Movement.DataStructures;
using Movement.Core.Inputs;
using Primitives.Physics;

namespace Movement.Core.Rules
{
    public static class ReloadRules
    {
        public static ReloadResult TryReload(ReloadRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            if (facts.IsGrounded || facts.IsOnPlatform)
            {
                return Approved();
            }
            return Denied();
        }

        private static ReloadResult Approved()
        {
            return new ReloadResult(true);
        }

        private static ReloadResult Denied()
        {
            return new ReloadResult(false);
        }

    }
}