
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;
using Core.Movement.Inputs;
using UnityEngine;
using Movement.Core.Abstractions;

namespace Movement.Core.Rules
{
    public static class LungeRules
    {
        public static LungeResult TryLunge(LungeRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            // Verify the rule state can be evaluated
            if (!ruleState.TryGet<IDisabledState>(out var disabledState)) return Denied();
            Debug.Log($"Passed disable check");
            if (!ruleState.TryGet<IStunState>(out var stunState)) return Denied();
            Debug.Log($"Passed stun check");
            if (!ruleState.TryGet<ILungeState>(out var lungeState)) return Denied();
            Debug.Log($"Passed lunge check");

            if (facts.IsGrounded || facts.IsOnPlatform)
            {
                Debug.Log($"Is grounded!");
                if (lungeState.LungeAmount > 0)
                {
                    lungeState.DecrementLunge();
                    lungeState.StartLungeCoolDownTimer();
                    return Approved(request);
                }
            }

            return Denied();
        }

        private static LungeResult Approved(LungeRequest request)
        {
            Debug.Log($"Lunge approved!");
            return new LungeResult(true, request.Direction);
        }

        private static LungeResult Denied()
        {
            Debug.Log($"Lunge Denied!");
            return new LungeResult(false, Vector2.right);
        }

    }
}