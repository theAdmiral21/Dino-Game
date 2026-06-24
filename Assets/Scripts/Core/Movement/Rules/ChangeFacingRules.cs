
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;
using Movement.Core.Abstractions;
using UnityEngine;
using Core.Movement.Inputs;

namespace Movement.Core.Rules
{
    public static class ChangeFacingRules
    {
        public static ChangeFacingResult TryChangeFacing(ChangeFacingRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            Debug.Log($"Evaluating changing facing");
            if (!ruleState.TryGet<IDirectionState>(out var dirState)) return new ChangeFacingResult();

            Debug.Log($"Forcing direction change");
            dirState.ForceDirection(request.FaceLeft);

            return new ChangeFacingResult();
        }

    }
}