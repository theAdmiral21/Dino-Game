using UnityEngine;
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;
using Movement.Core.Rules;
using Movement.Core.Abstractions;
using Movement.Core.Inputs;


namespace Movement.Core.Movement
{
    /// <summary>
    /// This class is used to evaluate the following rules for running:
    /// - The player can only run if the x input is not locked.
    /// - Running into walls halts movement.
    /// 
    /// </summary>
    public static class KnockBackRules
    {
        public static KnockBackResult TryKnockBack(KnockBackRequest request, PhysicsContext facts, IActorInput inputs, object ruleState)
        {
            // Verify the rule state can be evaluated
            if (!ruleState.TryGet<IDisabledState>(out var disabledState)) return Denied();
            // if (!ruleState.TryGet<IInvincibleState>(out var invincibleState)) return Denied();

            // if not request OR we're already stunned, deny
            if (!request.Requested || disabledState.IsDisabled) return Denied();


            // if (invincibleState.IsInvincible)
            // {
            //     // Debug.Log($"We're invincible?");
            //     return Denied();
            // }
            return Approved(request.ApexTime, request.Velocity);
        }

        private static KnockBackResult Approved(float apexTime, Vector2 velocity)
        {
            // Debug.Log($"Knock back approved - Frame: {Time.frameCount}");
            return new KnockBackResult(true, apexTime, velocity);
        }
        private static KnockBackResult Denied()
        {
            // Debug.Log($"Knock back denied - Frame: {Time.frameCount}");
            return new KnockBackResult(false, 0f, Vector2.zero);

        }
    }
}