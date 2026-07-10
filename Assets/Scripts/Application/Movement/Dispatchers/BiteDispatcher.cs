
using Movement.Application.Abstractions;
using Movement.Core.Rules;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;
using Core.Movement.Inputs;

namespace Movement.Application.Dispatchers
{
    public sealed class BiteDispatcher : DispatchRequestBase<BiteRequest, BiteResult>
    {
        protected override BiteResult Dispatch(in BiteRequest request, in PhysicsContext facts, in GameState gameState, in IActorInput inputs, object ruleState)
        {
            return BiteRules.TryBite(request, facts, inputs, ruleState);
        }
    }
}