
using Movement.Core.Movement.DataStructures;
using Movement.Core.Inputs;
using Primitives.Physics;
using Movement.Core.Abstractions;
using UnityEngine;

namespace Movement.Core.Rules
{
    public static class DodgeUpdateRules
    {
        public static DodgeUpdateResult TryDoggoDashUpdate(PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            if (!ruleState.TryGet<IDodgeState>(out var dodge)) return Denied();
            if (!ruleState.TryGet<IDisabledState>(out var disabledState)) return Denied();

            // Evaluate the rules
            if (disabledState.IsDisabled) return Denied();

            // NOTE I may need to add a buffer window for dashing in case the player ever does have multiple dashes.

            // If we are dashing
            if (dodge.IsDodging)
            {
                return Approved(dodge);
            }
            // Debug.Log($"Is not dashing");
            return Denied();
        }

        private static DodgeUpdateResult Approved(IDodgeState doggoDash)
        {
            // Debug.Log("Dash update approved");
            return new DodgeUpdateResult(true, doggoDash);
        }

        private static DodgeUpdateResult Denied()
        {
            // Debug.Log("Dash update denied");
            return new DodgeUpdateResult(false, null);
        }

    }
}