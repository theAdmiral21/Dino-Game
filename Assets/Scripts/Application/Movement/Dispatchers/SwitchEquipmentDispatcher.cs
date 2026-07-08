
using Movement.Application.Abstractions;
using Movement.Core.Rules;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;
using Core.Movement.Inputs;

namespace Movement.Application.Dispatchers
{
    public sealed class SwitchEquipmentDispatcher : DispatchRequestBase<SwitchEquipmentRequest, SwitchEquipmentResult>
    {
        protected override SwitchEquipmentResult Dispatch(in SwitchEquipmentRequest request, in PhysicsContext facts, in GameState gameState, in IActorInput inputs, object ruleState)
        {
            return SwitchEquipmentRules.TrySwitchEquipment(request, facts, inputs, ruleState);
        }
    }
}