using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Stats;
using Movement.Features.Movement.Abstractions;
using Primitives.Physics;
using Primitives.Stats.DataStructures;
using UnityEngine;

namespace Features.Movement.Services
{
    public class CalcLunge : ICalcAction
    {
        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult)
        {
            stats.TryGet<LungeStats>(out var lungeStats);

            if (actionResult is not LungeResult lunge || !actionResult.Approved) return currentResult;

            // Calculate the jump variables
            float gravity = -2 * lungeStats.LungeHeight.Value / Mathf.Pow(lungeStats.LungeApexTime.Value, 2);

            currentResult.Gravity = gravity;

            currentResult.Velocity.y = Mathf.Abs(gravity) * lungeStats.LungeApexTime.Value;

            currentResult.Velocity.x = lunge.Direction.x * (lungeStats.LungeDistance.Value / lungeStats.LungeDuration.Value);

            // Debug.Log($"Lunge velocity: {currentResult.Velocity}");

            return currentResult;
        }
    }
}