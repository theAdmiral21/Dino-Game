using Movement.Application.Abstractions;
using Movement.Core.Inputs;
using Movement.Core.Movement;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;
using UnityEngine;


namespace Movement.Application.Dispatchers
{
    public sealed class JumpDispatcher : DispatchRequestBase<JumpRequest, JumpResult>
    {
        protected override JumpResult Dispatch(in JumpRequest request, in PhysicsContext facts, in GameState gameState, in IActorInput inputs, object ruleState)
        {
            return JumpRules.TryJump(request, facts, inputs, ruleState);
        }
    }
}