
using Movement.Application.Abstractions;
using Movement.Core.Rules;
using Movement.Core.Inputs;
using Movement.Core.Movement;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;

namespace Movement.Application.Dispatchers
{
    public sealed class ReloadDispatcher : DispatchRequestBase<ReloadRequest, ReloadResult>
    {
        protected override ReloadResult Dispatch(in ReloadRequest request, in PhysicsContext facts, in GameState gameState, in IActorInput inputs, object ruleState)
        {
            return ReloadRules.TryReload(request, facts, inputs, ruleState);
        }
    }
}