using Movement.Core.Movement.Abstractions;
using Movement.Core.Stats;
using Primitives.Physics;

namespace Movement.Features.Movement.Abstractions
{
    public interface ICalcAction
    {
        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult);
    }
}