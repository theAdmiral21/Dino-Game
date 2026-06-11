
using Movement.Application.Abstractions;
using Movement.Core.Rules;
using Movement.Core.Inputs;
using Movement.Core.Movement;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;

namespace Movement.Application.Dispatchers
{
    public sealed class CrouchDispatcher : DispatchRequestBase<CrouchRequest, CrouchResult>
    {
        protected override CrouchResult Dispatch(in CrouchRequest request, in PhysicsContext facts, in GameState gameState, in IActorInput inputs, object ruleState)
        {
            return CrouchRules.TryCrouch(request, facts, inputs, ruleState);
        }
    }
}