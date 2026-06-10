using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Stats;
using Movement.Features.Movement.Abstractions;
using Primitives.Physics;
using Primitives.Stats.DataStructures;
using UnityEngine;

namespace Movement.Features.Movement.Services
{
    public class CalcQuickStepStop : ICalcAction
    {


        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult)
        {
            stats.TryGet<RunStats>(out var _runStats);
            stats.TryGet<SprintStats>(out var _sprintStats);
            if (actionResult is not QuickStepStopResult quickStepStop) return currentResult;

            if (!quickStepStop.Approved) return currentResult;

            // if the player is giving zero input => full stop
            float input = Mathf.Abs(quickStepStop.MoveInput.x);
            float inputDir = Mathf.Sign(quickStepStop.MoveInput.x);
            float target = 0;
            float accelValue = 0;
            if (input < 0.25f)
            {
                // Debug.Log($"reset velocity");
                currentResult.Velocity.x = 0;
            }
            else
            {
                // if the player is giving some input => top speed in the requested direction
                if (quickStepStop.SprintPressed)
                {
                    // Debug.Log($"set sprint vel");
                    target = inputDir * _sprintStats.SprintSpeed.Value;
                    accelValue = _sprintStats.SprintAccel.Value;
                }
                else
                {
                    // Debug.Log($"set run vel");
                    target = inputDir * _runStats.RunSpeed.Value;
                    accelValue = _runStats.RunSpeed.Value;
                }
                currentResult.Velocity.x = Mathf.MoveTowards(currentResult.Velocity.x, target, accelValue * currentResult.Dt);
            }
            Debug.Log($"Quick step stop vel: {currentResult.Velocity}");


            return currentResult;
        }

    }
}