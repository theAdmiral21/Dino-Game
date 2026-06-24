
using Movement.Application.Abstractions;
using Movement.Core.Rules;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;
using Core.Movement.Inputs;

namespace Movement.Application.Dispatchers
{
    public sealed class AimDispatcher : DispatchRequestBase<AimRequest, AimResult>
    {
        protected override AimResult Dispatch(in AimRequest request, in PhysicsContext facts, in GameState gameState, in IActorInput inputs, object ruleState)
        {
            return AimRules.TryAim(request, facts, inputs, ruleState);
        }
    }
}