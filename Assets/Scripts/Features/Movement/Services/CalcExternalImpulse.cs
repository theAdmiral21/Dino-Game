using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Stats;
using Movement.Features.Movement.Abstractions;
using Primitives.Physics;

namespace Movement.Features.Movement.Services
{
    public class CalcExternalImpulse : ICalcAction
    {
        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult)
        {
            if (actionResult is not ExternalImpulseResult impulse || !actionResult.Approved) return currentResult;

            currentResult.Gravity = impulse.Gravity;
            currentResult.Velocity.x = impulse.Velocity.x;
            currentResult.Velocity.y = impulse.Velocity.y;

            return currentResult;
        }

    }
}