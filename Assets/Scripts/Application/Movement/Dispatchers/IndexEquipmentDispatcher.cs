
using Movement.Application.Abstractions;
using Movement.Core.Rules;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;
using Core.Movement.Inputs;

namespace Movement.Application.Dispatchers
{
    public sealed class IndexEquipmentDispatcher : DispatchRequestBase<IndexEquipmentRequest, IndexEquipmentResult>
    {
        protected override IndexEquipmentResult Dispatch(in IndexEquipmentRequest request, in PhysicsContext facts, in GameState gameState, in IActorInput inputs, object ruleState)
        {
            return IndexEquipmentRules.TryIndexEquipment(request, facts, inputs, ruleState);
        }
    }
}