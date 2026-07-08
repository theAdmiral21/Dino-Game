using Core.Movement.Inputs;
using Movement.Core.Abstractions;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Movement.DataStructures;
using Primitives.Physics;

namespace Movement.Core.Rules
{
    public static class InteractRules
    {
        public static InteractResult TryInteract(InteractRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            if (!ruleState.TryGet<IDisabledState>(out var disabledState)) return Denied();
            if (!ruleState.TryGet<IStunState>(out var stunState)) return Denied();

            if (disabledState.IsDisabled || stunState.IsStunned) return Denied();

            return Approved();
        }

        private static InteractResult Approved()
        {
            return new InteractResult(true, Enums.ActionPhase.Override);
        }

        private static InteractResult Denied()
        {
            return new InteractResult(false, Enums.ActionPhase.Override);
        }

    }
}