
using Core.Movement.Inputs;
using Movement.Core.Abstractions;
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;

namespace Movement.Core.Rules
{
    public static class SwitchEquipmentRules
    {
        public static SwitchEquipmentResult TrySwitchEquipment(SwitchEquipmentRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            if (!ruleState.TryGet<IDisabledState>(out var disabledState)) return Denied();
            if (!ruleState.TryGet<IStunState>(out var stunState)) return Denied();

            if (disabledState.IsDisabled || stunState.IsStunned) return Denied();

            return Approved(request);
        }

        private static SwitchEquipmentResult Approved(SwitchEquipmentRequest request)
        {
            return new SwitchEquipmentResult(true, request.EquipmentNdx);
        }

        private static SwitchEquipmentResult Denied()
        {
            return new SwitchEquipmentResult(false, -1);
        }

    }
}