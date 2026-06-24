using UnityEngine;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Stats;
using Movement.Features.Movement.Abstractions;
using Primitives.Physics;
using Primitives.Stats.DataStructures;
using Primitives.Input;

namespace Movement.Features.Movement.Services
{
    public class CalcDodge : ICalcAction
    {
        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult)
        {
            stats.TryGet<DodgeStats>(out var _stats);
            if (actionResult is not DodgeResult dodge) return currentResult;

            if (!dodge.Approved) return currentResult;

            // Calculate the dash magnitude
            float dodgeSpeed = _stats.DodgeDist.Value / _stats.DodgeTime.Value;
            // Get the direction vector
            Vector2 dir = ConvertDirection(dodge.DodgeDirection);
            // Set the direction
            currentResult.Velocity = dodgeSpeed * dir;

            Debug.Log($"Dodge direction: {dir}");

            return currentResult;
        }

        private Vector2 ConvertDirection(InputDirection dash)
        {
            Debug.Log($"Dodge input direction: {dash}");

            if (dash == InputDirection.Right)
            {
                return Vector2.right;
            }
            return Vector2.left;
        }

    }
}