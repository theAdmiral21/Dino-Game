using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Stats;
using Movement.Features.Movement.Abstractions;
using Primitives.Physics;
using Primitives.Physics.Enums;
using Primitives.Stats.DataStructures;
using UnityEngine;

namespace Movement.Features.Movement.Services
{
    public class CalcClimb : ICalcAction
    {
        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult)
        {
            stats.TryGet<ClimbStats>(out ClimbStats climbStats);

            if (actionResult is not ClimbResult climb || !actionResult.Approved) return currentResult;

            float target = 0;
            float accelValue = 0;
            float dir = climb.InputDir.y > 0 ? 1f : -1f;
            float vDir = currentResult.Velocity.y >= 0 ? 1f : -1f;

            float yInput = climb.InputDir.y;
            if (climb.Climb == ClimbType.StairsTop || climb.Climb == ClimbType.StairsBottom)
            {
                target = climbStats.StairSpeed.Value * yInput;
                if (dir == vDir)
                {
                    accelValue = climbStats.StairAccel.Value;
                }
                else
                {
                    accelValue = climbStats.StairBrake.Value;
                }
            }
            else
            {
                target = climbStats.LadderSpeed.Value * yInput;
                if (dir == vDir)
                {
                    accelValue = climbStats.LadderAccel.Value;
                }
                else
                {
                    accelValue = climbStats.LadderBrake.Value;
                }
                // No sliding off of the ladder
                currentResult.Velocity.x = 0;
            }

            currentResult.Velocity.y = Mathf.MoveTowards(currentResult.Velocity.y, target, accelValue * currentResult.Dt);



            // Debug.Log($"Climb speed: {currentResult.Velocity.y}");

            return currentResult;
        }
    }
}