using UnityEngine;
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;
using Movement.Core.Rules;
using Movement.Core.Abstractions;
using Core.Movement.Inputs;
using Primitives.Physics.Enums;

namespace Movement.Core.Movement
{
    /// <summary>
    /// This class is used to evaluate the following rules for stopping a run:
    /// - The player can stop only when grounded
    /// 
    /// </summary>
    public static class ClimbStopRules
    {
        public static ClimbStopResult TryStopClimbStop(IActorInput inputs, PhysicsContext facts, object ruleState)
        {
            // Verify the rule state can be evaluated
            if (!ruleState.TryGet<IClimbState>(out var climbState)) return Denied(ClimbObject.None);

            if (Mathf.Abs(inputs.Move.y) < 0.25f)
                if (climbState.IsClimbing)
                {
                    return Approved(climbState.ClimbingSurface);
                }

            return Denied(climbState.ClimbingSurface);
        }


        private static ClimbStopResult Approved(ClimbObject climbingSurface)
        {
            Debug.Log($"Climb stop approved");
            return new ClimbStopResult(true, climbingSurface);
        }
        private static ClimbStopResult Denied(ClimbObject climbingSurface)
        {
            Debug.Log("Climb stop denied");
            return new ClimbStopResult(false, climbingSurface);

        }
    }
}