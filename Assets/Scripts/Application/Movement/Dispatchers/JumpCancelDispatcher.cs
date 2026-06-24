using Core.Movement.Inputs;
using Movement.Application.Abstractions;
using Movement.Core.Movement;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;

namespace Movement.Application.Dispatchers
{
    public sealed class JumpCancelDispatcher : DispatchRequestBase<JumpCancelRequest, JumpCancelResult>
    {
        protected override JumpCancelResult Dispatch(
            in JumpCancelRequest request,
             in PhysicsContext facts,
              in GameState gameState,
               in IActorInput inputs,
                object ruleState)
        {
            return JumpCancelRules.TryJumpCancel(request, facts, ruleState);
        }
    }
}