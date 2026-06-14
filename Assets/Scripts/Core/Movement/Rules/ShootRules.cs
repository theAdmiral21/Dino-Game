
using Movement.Core.Movement.DataStructures;
using Movement.Core.Inputs;
using Primitives.Physics;
using Movement.Core.Abstractions;

namespace Movement.Core.Rules
{
    public static class ShootRules
    {
        public static ShootResult TryShoot(ShootRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            if (!ruleState.TryGet<IAimingState>(out var aimingState)) return Denied();

            if (aimingState.IsAiming) return Approved();

            return Denied();

        }

        private static ShootResult Approved()
        {
            return new ShootResult(true, Enums.ActionPhase.Impulse);
        }

        private static ShootResult Denied()
        {
            return new ShootResult(false, Enums.ActionPhase.Impulse);
        }

    }
}