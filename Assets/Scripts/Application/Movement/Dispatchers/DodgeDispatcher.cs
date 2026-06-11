
using Movement.Application.Abstractions;
using Movement.Core.Rules;
using Movement.Core.Inputs;
using Movement.Core.Movement;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;

namespace Movement.Application.Dispatchers
{
    public sealed class DodgeDispatcher : DispatchRequestBase<DodgeRequest, DodgeResult>
    {
        protected override DodgeResult Dispatch(in DodgeRequest request, in PhysicsContext facts, in GameState gameState, in IActorInput inputs, object ruleState)
        {
            return DodgeRules.TryDodge(request, facts, inputs, ruleState);
        }
    }
}