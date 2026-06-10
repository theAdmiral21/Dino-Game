using Movement.Core.Movement.DataStructures;
using Movement.Core.Enums;
using Primitives.Physics;
using Movement.Core.Rules;
using Movement.Core.Abstractions;
using Movement.Core.Inputs;
using UnityEngine;

namespace Movement.Core.Movement
{
    public static class QuickStepUpdateRules
    {
        public static QuickStepUpdateResult TryQuickStepUpdate(PhysicsContext facts, IActorInput inputs, object ruleState)
        {
            // Basic checks
            // Debug.Log($"rule state: {ruleState}");
            if (!ruleState.TryGet<IDisabledState>(out var disabledState)) return Denied();
            if (!ruleState.TryGet<IQuickStepState>(out var quickStepState)) return Denied();

            // While the quick step is active update it
            if (!quickStepState.QuickStepActive || disabledState.IsDisabled) return Denied();

            float dir = Mathf.Sign(inputs.Move.x);

            return Approved(quickStepState);
        }

        private static QuickStepUpdateResult Approved(IQuickStepState quickStepState)
        {
            // Debug.Log("QuickStep update approved");
            return new QuickStepUpdateResult(true, quickStepState, ActionPhase.Continuous);
        }
        private static QuickStepUpdateResult Denied()
        {
            // Debug.Log("QuickStep update denied");
            return new QuickStepUpdateResult(false, null, ActionPhase.Continuous);

        }
    }
}