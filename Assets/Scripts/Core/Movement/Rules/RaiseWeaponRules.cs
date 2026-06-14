
using Movement.Core.Movement.DataStructures;
using Movement.Core.Inputs;
using Primitives.Physics;
using Movement.Core.Abstractions;

namespace Movement.Core.Rules
{
    public static class RaiseWeaponRules
    {
        public static RaiseWeaponResult TryRaiseWeapon(RaiseWeaponRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            if (!ruleState.TryGet<IAimingState>(out var aimingState)) return Denied();

            if (facts.IsGrounded || facts.IsOnPlatform)
            {
                aimingState.SetAiming(true);
                return Approved();
            }
            aimingState.SetAiming(false);
            return Denied();
        }

        private static RaiseWeaponResult Approved()
        {
            return new RaiseWeaponResult(true, Enums.ActionPhase.Continuous);
        }

        private static RaiseWeaponResult Denied()
        {
            return new RaiseWeaponResult(false, Enums.ActionPhase.Continuous);
        }

    }
}