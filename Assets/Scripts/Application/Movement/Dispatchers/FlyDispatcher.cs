using Movement.Application.Abstractions;
using Movement.Core.Inputs;
using Movement.Core.Movement;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;


namespace Movement.Application.Dispatchers
{
    public sealed class FlyDispatcher : DispatchRequestBase<FlyRequest, FlyResult>
    {
        protected override FlyResult Dispatch(in FlyRequest request, in PhysicsContext facts, in GameState gameState, in IActorInput inputs, object ruleState)
        {
            return FlyRules.TryFly(request, facts, inputs, ruleState);
        }
    }
}