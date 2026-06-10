
using Movement.Core.Movement.DataStructures;
using Movement.Core.Inputs;
using Primitives.Physics;
using Movement.Core.Abstractions;
using UnityEditor.ShaderKeywordFilter;

namespace Movement.Core.Rules
{
    public static class SwitchMovementRules
    {
        /// <summary>
        /// Debug action for testing double jump and dashing 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="facts"></param>
        /// <param name="inputValues"></param>
        /// <param name="ruleState"></param>
        /// <returns></returns>
        public static SwitchMovementResult TrySwitchMovement(SwitchMovementRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            if (!ruleState.TryGet<ISwitchMovement>(out var switchMovement)) return Denied();

            switchMovement.SwitchMovement();

            return Approved();
        }

        private static SwitchMovementResult Approved()
        {
            return new SwitchMovementResult();
        }

        private static SwitchMovementResult Denied()
        {
            return new SwitchMovementResult();
        }

    }
}