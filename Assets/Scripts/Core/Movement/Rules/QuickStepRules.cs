using Movement.Core.Movement.DataStructures;
using Movement.Core.Enums;
using Primitives.Physics;
using Movement.Core.Rules;
using Movement.Core.Abstractions;
using Movement.Core.Inputs;

namespace Movement.Core.Movement
{
    public static class QuickStepRules
    {
        public static QuickStepResult TryQuickStep(QuickStepRequest request, PhysicsContext facts, IActorInput inputs, object ruleState)
        {
            // Basic checks
            // Debug.Log($"rule state: {ruleState}");
            if (!ruleState.TryGet<IDisabledState>(out var disabledState)) return Denied();
            if (!ruleState.TryGet<IXInputState>(out var xInputState)) return Denied();
            if (!ruleState.TryGet<IQuickStepState>(out var quickStepState)) return Denied();

            if (!request.Requested || disabledState.IsDisabled) return Denied();

            if (facts.IsGrounded || facts.IsOnPlatform)
            {
                if (quickStepState.QuickStepReady)
                {
                    quickStepState.StartQuickStepTimer();
                    quickStepState.SetQuickStepDirection(request.Direction);
                    xInputState.StartBlockXTimer();
                    return Approved(request.Direction);
                }
            }

            return Denied();
        }

        private static QuickStepResult Approved(float direction)
        {
            // Debug.Log("QuickStep approved");
            return new QuickStepResult(true, direction, ActionPhase.Impulse);
        }
        private static QuickStepResult Denied()
        {
            // Debug.Log("QuickStep denied");
            return new QuickStepResult(false, 1, ActionPhase.Impulse);

        }
    }
}