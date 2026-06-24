
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;
using Movement.Core.Abstractions;
using UnityEngine;
using Core.Movement.Inputs;

namespace Movement.Core.Rules
{
    public static class CrouchRules
    {
        public static CrouchResult TryCrouch(CrouchRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            if (!ruleState.TryGet<ICrouchState>(out var crouchState)) return Denied();
            Debug.LogError($"This will eventually need to perform some sort of space check before changing the collider");
            if (facts.IsGrounded || facts.IsOnPlatform)
            {
                bool newCrouchValue = !crouchState.IsCrouching;
                crouchState.SetCrouchState(newCrouchValue);
                return Approved(newCrouchValue);
            }
            crouchState.SetCrouchState(false);
            return Denied();
        }

        private static CrouchResult Approved(bool newCrouchValue)
        {
            Debug.Log($"Approved crouch");
            return new CrouchResult(true, newCrouchValue);
        }

        private static CrouchResult Denied()
        {
            Debug.Log($"Denied crouch");
            return new CrouchResult(false, false);
        }

    }
}