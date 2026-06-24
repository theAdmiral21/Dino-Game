
using Movement.Application.Abstractions;
using Movement.Core.Rules;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;
using Core.Movement.Inputs;

namespace Movement.Application.Dispatchers
{
    public sealed class ExternalContinuousDispatcher : DispatchRequestBase<ExternalContinuousRequest, ExternalContinuousResult>
    {
        protected override ExternalContinuousResult Dispatch(in ExternalContinuousRequest request, in PhysicsContext facts, in GameState gameState, in IActorInput inputs, object ruleState)
        {
            return ExternalContinuousRules.TryExternalContinuous(request, facts, inputs, ruleState);
        }
    }
}