using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Stats;
using Movement.Features.Movement.Abstractions;
using Primitives.Physics;
using Primitives.Stats.DataStructures;
using UnityEngine;

namespace Movement.Features.Movement.Services
{
    public class CalcFriction : ICalcAction
    {


        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult)
        {
            if (!stats.TryGet<FrictionStats>(out var frictionStats)) return currentResult;

            if (actionResult is not FrictionResult friction) return currentResult;

            // Start slowing down
            float target = 0;
            float frictionAccel = frictionStats.Friction.Value;

            currentResult.Velocity.x = Mathf.MoveTowards(currentResult.Velocity.x, target, frictionAccel * currentResult.Dt);

            return currentResult;
        }

    }
}