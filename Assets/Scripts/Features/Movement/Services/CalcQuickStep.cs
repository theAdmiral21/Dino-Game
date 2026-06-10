using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Stats;
using Movement.Features.Movement.Abstractions;
using Primitives.Physics;
using Primitives.Stats.DataStructures;

namespace Movement.Features.Movement.Services
{
    public class CalcQuickStep : ICalcAction
    {


        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult)
        {
            stats.TryGet<QuickStepStats>(out var _stats);
            if (actionResult is not QuickStepResult quickStep) return currentResult;

            if (!quickStep.Approved) return currentResult;

            // Calculate a quick step
            currentResult.Velocity.x = (_stats.QuickStepDistance.Value / _stats.QuickStepDuration.Value) * quickStep.Direction;


            return currentResult;
        }

    }
}