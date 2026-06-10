
using Movement.Core.Movement.DataStructures;
using Movement.Core.Inputs;
using Primitives.Physics;
using UnityEngine;

namespace Movement.Core.Rules
{
    public static class ExternalContinuousRules
    {
        public static ExternalContinuousResult TryExternalContinuous(ExternalContinuousRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            return Approved(request.Velocity);
        }

        private static ExternalContinuousResult Approved(Vector2 velocity)
        {
            return new ExternalContinuousResult(true, velocity);
        }

        private static ExternalContinuousResult Denied()
        {
            return new ExternalContinuousResult(true, Vector2.zero);
        }

    }
}