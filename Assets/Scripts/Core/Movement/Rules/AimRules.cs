
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;
using Movement.Core.Abstractions;
using UnityEngine;
using Core.Movement.Inputs;

namespace Movement.Core.Rules
{
    public static class AimRules
    {
        public static AimResult TryAim(AimRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            if (!ruleState.TryGet<IAimingState>(out var aimingState)) return Denied();

            if (aimingState.IsAiming && (facts.IsGrounded || facts.IsOnPlatform))
            {
                return Approved(request);
            }
            return Denied();
        }

        private static AimResult Approved(AimRequest request)
        {
            Debug.Log($"Aim approved");
            return new AimResult(true, request.MousePosition);
        }

        private static AimResult Denied()
        {
            Debug.Log($"Aim denied");
            return new AimResult(false, Vector2.zero);
        }

    }
}