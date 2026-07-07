using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Stats;
using Movement.Features.Movement.Abstractions;
using Primitives.Physics;
using Primitives.Physics.Enums;
using Primitives.Stats.DataStructures;
using UnityEngine;

namespace Features.Movement.Services
{
    public class CalcClimbStop : ICalcAction
    {
        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult)
        {
            stats.TryGet<ClimbStats>(out var climbStats);

            if (actionResult is not ClimbStopResult climbStop || !actionResult.Approved) return currentResult;

            // Stop climbing

            float target = 0f;
            float accelValue = 0f;



            if (climbStop.ClimbingSurface == ClimbObject.Ladder)
            {
                accelValue = climbStats.LadderBrake.Value;
            }
            else
            {
                accelValue = climbStats.StairBrake.Value;

            }

            currentResult.Velocity.y = Mathf.MoveTowards(currentResult.Velocity.y, target, accelValue * currentResult.Dt);


            return currentResult;
        }
    }
}