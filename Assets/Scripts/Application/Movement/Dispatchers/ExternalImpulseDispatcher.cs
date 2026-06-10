
using Movement.Application.Abstractions;
using Movement.Core.Rules;
using Movement.Core.Inputs;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;

namespace Movement.Application.Dispatchers
{
    public sealed class ExternalImpulseDispatcher : DispatchRequestBase<ExternalImpulseRequest, ExternalImpulseResult>
    {
        protected override ExternalImpulseResult Dispatch(in ExternalImpulseRequest request, in PhysicsContext facts, in GameState gameState, in IActorInput inputs, object ruleState)
        {
            return ExternalImpulseRules.TryExternalImpulse(request, facts, inputs, ruleState);
        }
    }
}