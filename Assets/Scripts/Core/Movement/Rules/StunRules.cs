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
    public static class StunRules
    {
        public static StunResult TryStun(StunRequest request, PhysicsContext facts, IActorInput inputs, object ruleState)
        {
            // Verify the rule state can be evaluated
            if (!ruleState.TryGet<IStunState>(out var stunState)) return Denied();
            if (!ruleState.TryGet<IDisabledState>(out var disabledState)) return Denied();

            // if not request OR we're already stunned, deny
            if (!request.Requested || disabledState.IsDisabled) return Denied();

            Debug.Log("Starting stun timer");
            stunState.StartStunnedTimer(request.Duration);

            return Approved();
        }

        private static StunResult Approved()
        {
            // Debug.Log("Run approved");
            return new StunResult(true);
        }
        private static StunResult Denied()
        {
            Debug.Log("Stun denied");
            return new StunResult(false);

        }
    }
}