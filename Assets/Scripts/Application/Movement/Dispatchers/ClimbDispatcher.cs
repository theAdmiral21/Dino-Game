
using Movement.Application.Abstractions;
using Movement.Core.Rules;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;
using Core.Movement.Inputs;

namespace Movement.Application.Dispatchers
{
    public sealed class ClimbDispatcher : DispatchRequestBase<ClimbRequest, ClimbResult>
    {
        protected override ClimbResult Dispatch(in ClimbRequest request, in PhysicsContext facts, in GameState gameState, in IActorInput inputs, object ruleState)
        {
            return ClimbRules.TryClimb(request, facts, inputs, ruleState);
        }
    }
}