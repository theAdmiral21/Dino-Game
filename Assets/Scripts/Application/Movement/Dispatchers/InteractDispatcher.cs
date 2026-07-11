
using Movement.Application.Abstractions;
using Movement.Core.Rules;
using Primitives.GameState;
using Primitives.Physics;
using Core.Movement.Inputs;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.Movement.Abstractions;

namespace Movement.Application.Dispatchers
{
    public sealed class InteractDispatcher : DispatchRequestBase<InteractRequest, InteractResult>
    {
        protected override InteractResult Dispatch(in InteractRequest request, in PhysicsContext facts, in GameState gameState, in IActorInput inputs, object ruleState)
        {
            return InteractRules.TryInteract(request, facts, inputs, ruleState);
        }
    }
}