using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Stats;
using Movement.Features.Movement.Abstractions;
using Primitives.Physics;

namespace Movement.Features.Movement.Services
{
    public class CalcExternalContinuous : ICalcAction
    {
        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult)
        {
            if (actionResult is not ExternalContinuousResult cont || !actionResult.Approved) return currentResult;
            // Debug.Log("Evaluating external continuous force");
            currentResult.ExternalVelocity.x = cont.Velocity.x;
            currentResult.ExternalVelocity.y = cont.Velocity.y;
            return currentResult;
        }

    }
}