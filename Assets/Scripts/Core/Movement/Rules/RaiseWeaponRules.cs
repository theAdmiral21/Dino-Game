
using Movement.Core.Movement.DataStructures;
using Movement.Core.Inputs;
using Primitives.Physics;
using Movement.Core.Abstractions;
using UnityEngine;

namespace Movement.Core.Rules
{
    public static class RaiseWeaponRules
    {
        public static RaiseWeaponResult TryRaiseWeapon(RaiseWeaponRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            if (!ruleState.TryGet<IAimingState>(out var aimingState)) return Denied();

            if (facts.IsGrounded || facts.IsOnPlatform)
            {
                aimingState.SetAiming(request.SetAiming);
                return Approved();
            }
            aimingState.SetAiming(false);
            return Denied();
        }

        private static RaiseWeaponResult Approved()
        {
            Debug.Log($"Raise weapon approved");
            return new RaiseWeaponResult(true, Enums.ActionPhase.Continuous);
        }

        private static RaiseWeaponResult Denied()
        {
            Debug.Log($"Raise weapon denied");
            return new RaiseWeaponResult(false, Enums.ActionPhase.Continuous);
        }

    }
}