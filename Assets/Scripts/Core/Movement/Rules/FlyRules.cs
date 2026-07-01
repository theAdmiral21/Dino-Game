using UnityEngine;
using Movement.Core.Movement.DataStructures;
using Movement.Core.State.DataStructures;
using Primitives.Physics;
using Movement.Core.Rules;
using Movement.Core.Abstractions;
using Core.Movement.Inputs;

namespace Movement.Core.Movement
{
    /// <summary>
    /// This class is used to evaluate the following rules for running:
    /// - The player can only run if the x input is not locked.
    /// - Running into walls halts movement.
    /// 
    /// </summary>
    public static class FlyRules
    {
        public static FlyResult TryFly(FlyRequest request, PhysicsContext facts, IActorInput inputs, object ruleState)
        {
            // Verify the rule state can be evaluated
            if (!ruleState.TryGet<IDisabledState>(out var disabledState)) return Denied();
            // Debug.Log($"Passed disabled check");
            if (!ruleState.TryGet<IStunState>(out var stunState)) return Denied();
            // Debug.Log($"Passed stun check");

            // Basic checks
            if (!request.Requested || disabledState.IsDisabled || stunState.IsStunned) return Denied();

            // Uh fly I guess?

            return Approved(request.Value);
        }

        private static void SetDirection(RunRequest request, PlayerRuleState ruleState)
        {
            var reqDir = Mathf.Sign(request.Value.x);
            if (reqDir == 1)
            {
                // ruleState.SetDirection(1);
            }
            else if (reqDir == -1)
            {
                // ruleState.SetDirection(-1);
            }
        }
        private static FlyResult Approved(Vector2 value)
        {
            // Debug.Log("Fly approved");
            return new FlyResult(true, value);
        }
        private static FlyResult Denied()
        {
            // Debug.Log("Fly denied");
            return new FlyResult(false, Vector2.zero);

        }
    }
}