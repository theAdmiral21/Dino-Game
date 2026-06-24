
using Core.Movement.Inputs;
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;
using UnityEngine;

namespace Movement.Core.Rules
{
    public static class RotateRules
    {
        public static RotateResult TryRotate(RotateRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            // Uh I don't know when something wouldn't be able to rotate..
            return Approved(request.Omega);
        }

        private static RotateResult Approved(float omega)
        {
            Debug.Log($"Approved rotation");
            return new RotateResult(true, omega, Enums.ActionPhase.Continuous);
        }

        private static RotateResult Denied()
        {
            Debug.Log($"Denied rotation");
            return new RotateResult(false, 0, Enums.ActionPhase.Continuous);
        }

    }
}