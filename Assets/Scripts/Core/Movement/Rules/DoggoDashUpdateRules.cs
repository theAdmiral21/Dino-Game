
using Movement.Core.Movement.DataStructures;
using Movement.Core.Inputs;
using Primitives.Physics;
using Movement.Core.Abstractions;
using UnityEngine;

namespace Movement.Core.Rules
{
    public static class DoggoDashUpdateRules
    {
        public static DoggoDashUpdateResult TryDoggoDashUpdate(PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            if (!ruleState.TryGet<IDoggoDashState>(out var doggoDash)) return Denied();
            if (!ruleState.TryGet<IDisabledState>(out var disabledState)) return Denied();
            if (!ruleState.TryGet<ISwitchMovement>(out var switchMovement)) return Denied();

            // Evaluate the rules
            if (disabledState.IsDisabled) return Denied();
            if (!switchMovement.DashMode) return Denied();

            // NOTE I may need to add a buffer window for dashing in case the player ever does have multiple dashes.

            // If we are dashing
            if (doggoDash.IsDashing)
            {
                return Approved(doggoDash);
            }
            // Debug.Log($"Is not dashing");
            return Denied();
        }

        private static DoggoDashUpdateResult Approved(IDoggoDashState doggoDash)
        {
            // Debug.Log("Dash update approved");
            return new DoggoDashUpdateResult(true, doggoDash);
        }

        private static DoggoDashUpdateResult Denied()
        {
            // Debug.Log("Dash update denied");
            return new DoggoDashUpdateResult(false, null);
        }

    }
}