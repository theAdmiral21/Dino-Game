using Movement.Application.Abstractions;
using Movement.Core.Inputs;
using Movement.Core.Movement;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;


namespace Movement.Application.Dispatchers
{
    public class KnockBackDispatcher : DispatchRequestBase<KnockBackRequest, KnockBackResult>
    {

        protected override KnockBackResult Dispatch(in KnockBackRequest request, in PhysicsContext facts, in GameState gameState, in IActorInput inputs, object ruleState)
        {
            // Debug.Log("Dispatching run");
            return KnockBackRules.TryKnockBack(request, facts, inputs, ruleState);

        }
    }
}