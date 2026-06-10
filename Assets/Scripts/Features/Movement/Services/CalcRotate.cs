using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Stats;
using Movement.Features.Movement.Abstractions;
using Primitives.Physics;
using Primitives.Stats.DataStructures;
using UnityEngine;

namespace Movement.Features.Movement.Services
{
    public class CalcRotate : ICalcAction
    {


        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult)
        {

            stats.TryGet<RotateStats>(out var rotateStats);

            if (actionResult is not RotateResult rotate || !actionResult.Approved) return currentResult;

            float target = 0;
            float accelValue = rotateStats.AngularAccel.Value;

            // Calculate the rotation speed
            target = rotate.Omega;


            // Smooth things out
            // Debug.Log($"accel value: {accelValue}");
            currentResult.Velocity.x = Mathf.MoveTowards(currentResult.RotationalVelocity, target, accelValue * currentResult.Dt);
            // currentResult.Velocity.x = target;

            return currentResult;
        }
    }
}