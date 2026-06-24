using Core.Movement.Inputs;
using Movement.Application.Abstractions;
using Movement.Core.Movement.DataStructures;
using PlayerController.Core.Movement;
using PlayerController.Core.Movement.Abstractions;
using Primitives.GameState;
using Primitives.Physics;


namespace PlayerController.Application.Movement.Dispatchers
{
    public sealed class PauseDispatcher : DispatchRequestBase<PauseRequest, PauseResult>
    {
        protected override PauseResult Dispatch(in PauseRequest request, in PhysicsContext facts, in GameState gameState, in IActorInput inputs, object ruleState)
        {
            return PauseRules.TryPause(request, facts, gameState, inputs, ruleState);
        }
    }
}