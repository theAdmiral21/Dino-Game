using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Stats;
using Movement.Features.Movement.Abstractions;
using Primitives.Physics;
using Primitives.Stats.DataStructures;

namespace Movement.Features.Movement.Services
{
    public class CalcJumpCancel : ICalcAction
    {


        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult)
        {
            stats.TryGet<GravityStats>(out var _stats);
            if (actionResult is not JumpCancelResult jumpCancel) return currentResult;

            if (!jumpCancel.Approved) return currentResult;

            // Calculate the jump cancel variables
            currentResult.Gravity = _stats.BaseGravity.Value * _stats.SlowFall.Value;
            currentResult.Velocity.y = 0f;
            return currentResult;
        }

    }
}