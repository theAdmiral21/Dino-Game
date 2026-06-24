using Core.Movement.Inputs;
using Movement.Application.Abstractions;
using Movement.Core.Movement;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;


namespace Movement.Application.Dispatchers
{
    public class StunDispatcher : DispatchRequestBase<StunRequest, StunResult>
    {

        protected override StunResult Dispatch(in StunRequest request, in PhysicsContext facts, in GameState gameState, in IActorInput inputs, object ruleState)
        {
            // Debug.Log("Dispatching run");
            return StunRules.TryStun(request, facts, inputs, ruleState);

        }
    }
}