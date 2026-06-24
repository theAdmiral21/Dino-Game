
using Core.Movement.Inputs;
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;
using UnityEngine;

namespace Movement.Core.Rules
{
    public static class ExternalImpulseRules
    {
        public static ExternalImpulseResult TryExternalImpulse(ExternalImpulseRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            // I actually have no idea when this would ever be denied..
            return Approved(request.Velocity, request.Gravity);
        }

        private static ExternalImpulseResult Approved(Vector2 velocity, float gravity)
        {
            Debug.Log($"External impulse approved");
            return new ExternalImpulseResult(true, velocity, gravity);
        }

        private static ExternalImpulseResult Denied()
        {
            return new ExternalImpulseResult(true, Vector2.zero, 0);
        }

    }
}