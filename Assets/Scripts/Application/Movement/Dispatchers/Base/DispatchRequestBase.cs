using System;
using Movement.Core.Inputs;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;

namespace Movement.Application.Abstractions
{
    public abstract class DispatchRequestBase<TRequest, TResult> : IDispatchRequest
    where TRequest : struct, IActionRequest
    where TResult : struct, IActionResult
    {
        public Type DispatchType => typeof(TRequest);

        public IActionResult DispatchUntyped(
            IActionRequest request,
            in PhysicsContext facts,
            in GameState gameState,
            in IActorInput inputs,
            object ruleState)
        {
            return Dispatch(
            (TRequest)request,
            facts,
            gameState,
            inputs,
            ruleState);
        }

        protected abstract TResult Dispatch(
            in TRequest request,
            in PhysicsContext facts,
            in GameState gameState,
            in IActorInput inputs,
            object ruleState);
    }
}