using System;
using Core.Movement.Inputs;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;

namespace Movement.Application.Abstractions
{
    public interface IDispatchRequest
    {
        public Type DispatchType { get; }

        IActionResult DispatchUntyped(
            IActionRequest request,
            in PhysicsContext facts,
            in GameState gameState,
            in IActorInput inputs,
            object ruleState);
    }
}