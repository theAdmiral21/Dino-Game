using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Stats;
using Movement.Features.Movement.Abstractions;
using Primitives.Physics;
using Primitives.Stats.DataStructures;
using UnityEngine;

namespace Movement.Features.Movement.Services
{
    public class CalcFly : ICalcAction
    {


        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult)
        {
            bool check = stats.TryGet<RunStats>(out RunStats _stats);
            Debug.Assert(check == true, "Unable to calculate fly state. Missing RunStats");
            // Debug.Log("Calculate flight");
            if (actionResult is not FlyResult fly || !actionResult.Approved) return currentResult;

            var input = new Vector2(fly.Input.x, fly.Input.y);
            if (input.sqrMagnitude > 1f)
                input.Normalize();

            currentResult.Velocity.x = input.x * _stats.RunSpeed.Value;
            currentResult.Velocity.y = input.y * _stats.RunSpeed.Value;

            return currentResult;
        }
    }
}