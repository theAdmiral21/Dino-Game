using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Stats;
using Movement.Features.Movement.Abstractions;
using Primitives.Physics;
using Primitives.Stats.DataStructures;
using UnityEngine;

namespace Movement.Features.Movement.Services
{
    public class CalcRunStop : ICalcAction
    {
        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult)
        {
            stats.TryGet<RunStats>(out var _stats);
            stats.TryGet<AerialStats>(out var _aerialStats);

            if (actionResult is not RunStopResult runStop || !actionResult.Approved) return currentResult;

            // Stop running

            float target = 0f;
            float accelValue = 0f;



            if (runStop.RunType == RunType.Aerial)
            {
                accelValue = _aerialStats.AerialBrake.Value;
            }
            else
            {
                accelValue = _stats.BrakeAccel.Value;

            }

            currentResult.Velocity.x = Mathf.MoveTowards(currentResult.Velocity.x, target, accelValue * currentResult.Dt);


            return currentResult;
        }
    }
}