using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Stats;
using Movement.Features.Movement.Abstractions;
using Primitives.Physics;
using Primitives.Stats.DataStructures;
using UnityEngine;

namespace Movement.Features.Movement.Services
{
    public class CalcLanding : ICalcAction
    {


        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult)
        {

            stats.TryGet<RunStats>(out var _stats);
            if (actionResult is not LandingResult landing || !actionResult.Approved) return currentResult;

            // We've landed stop vertical velocity
            if (currentResult.Velocity.y < 0)
            {
                currentResult.Velocity.y = 0;
                currentResult.Gravity = 0f;
            }


            // Stop running
            // currentResult.Velocity.x = 0f;
            float target = 0f;


            float accelValue = _stats.BrakeAccel.Value;

            currentResult.Velocity.x = Mathf.MoveTowards(currentResult.Velocity.x, target, accelValue * currentResult.Dt);

            return currentResult;
        }
    }
}