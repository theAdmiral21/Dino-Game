
using Core.Movement.Inputs;
using Movement.Core.Abstractions;
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;

namespace Movement.Core.Rules
{
    public static class IndexEquipmentRules
    {
        public static IndexEquipmentResult TryIndexEquipment(IndexEquipmentRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            if (!ruleState.TryGet<IDisabledState>(out var disabledState)) return Denied();
            if (!ruleState.TryGet<IStunState>(out var stunState)) return Denied();

            if (disabledState.IsDisabled || stunState.IsStunned) return Denied();

            return Approved(request);
        }

        private static IndexEquipmentResult Approved(IndexEquipmentRequest request)
        {
            return new IndexEquipmentResult(true, request.DeltaNdx);
        }

        private static IndexEquipmentResult Denied()
        {
            return new IndexEquipmentResult(false, 0);
        }

    }
}