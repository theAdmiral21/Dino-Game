
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;
using Core.Movement.Inputs;
using Movement.Core.Abstractions;
using UnityEngine;

namespace Movement.Core.Rules
{
    public static class BiteRules
    {
        public static BiteResult TryBite(BiteRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            // Verify the rule state can be evaluated
            if (!ruleState.TryGet<IDisabledState>(out var disabledState)) return Denied();
            if (!ruleState.TryGet<IStunState>(out var stunState)) return Denied();

            if (facts.IsGrounded || facts.IsOnPlatform)
            {
                return Approved();
            }

            return Denied();
        }

        private static BiteResult Approved()
        {
            Debug.Log($"Bite approved");
            return new BiteResult(true);
        }

        private static BiteResult Denied()
        {
            Debug.Log($"Bite denied");
            return new BiteResult(false);
        }

    }
}