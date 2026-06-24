
using Movement.Application.Abstractions;
using Movement.Core.Rules;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;
using Core.Movement.Inputs;

namespace Movement.Application.Dispatchers
{
    public sealed class FrictionDispatcher : DispatchRequestBase<FrictionRequest, FrictionResult>
    {
        protected override FrictionResult Dispatch(in FrictionRequest request, in PhysicsContext facts, in GameState gameState, in IActorInput inputs, object ruleState)
        {
            return FrictionRules.TryFriction(facts, ruleState);
        }
    }
}