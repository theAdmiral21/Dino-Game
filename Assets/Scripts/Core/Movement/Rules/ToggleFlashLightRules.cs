
using Core.Movement.Inputs;
using Movement.Core.Abstractions;
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;

namespace Movement.Core.Rules
{
    public static class ToggleFlashLightRules
    {
        public static ToggleFlashLightResult TryToggleFlashLight(ToggleFlashLightRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            if (!ruleState.TryGet<IDisabledState>(out var disabledState)) return Denied();
            if (!ruleState.TryGet<IStunState>(out var stunState)) return Denied();
            if (!ruleState.TryGet<IClimbState>(out var climbState)) return Denied();

            if (disabledState.IsDisabled || stunState.IsStunned || climbState.IsClimbing) return Denied();

            return Approved();
        }

        private static ToggleFlashLightResult Approved()
        {
            return new ToggleFlashLightResult(true);
        }

        private static ToggleFlashLightResult Denied()
        {
            return new ToggleFlashLightResult(false);
        }

    }
}