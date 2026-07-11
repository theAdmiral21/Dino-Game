
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;
using Movement.Core.Abstractions;
using Core.Movement.Inputs;

namespace Movement.Core.Rules
{
    public static class RaiseWeaponRules
    {
        public static RaiseWeaponResult TryRaiseWeapon(RaiseWeaponRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            if (!ruleState.TryGet<IAimingState>(out var aimingState)) return Denied();
            // Did the player ask to aim?
            if (request.SetAiming && (facts.IsGrounded || facts.IsOnPlatform))
            {
                aimingState.SetAiming(true);
                return Approve(true);
            }
            // lower the weapon
            aimingState.SetAiming(false);
            return Approve(false);
        }

        private static RaiseWeaponResult Approve(bool isAiming)
        {
            // Debug.Log($"Raise weapon approved");
            if (isAiming)
            {
                return new RaiseWeaponResult(true, true, Enums.ActionPhase.Continuous);
            }
            return new RaiseWeaponResult(true, false, Enums.ActionPhase.Continuous);
        }

        private static RaiseWeaponResult Denied()
        {
            // Debug.Log($"Raise weapon denied");
            return new RaiseWeaponResult(false, false, Enums.ActionPhase.Continuous);
        }

    }
}