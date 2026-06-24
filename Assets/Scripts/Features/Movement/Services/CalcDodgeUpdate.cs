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
    public class CalcDodgeUpdate : ICalcAction
    {
        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult)
        {
            stats.TryGet<DodgeStats>(out var _stats);
            if (actionResult is not DodgeUpdateResult dodge) return currentResult;

            if (!dodge.Approved) return currentResult;

            // Ease the dash
            var dodgeState = dodge.DashState;

            float t = dodgeState.DodgeCounter / dodgeState.DodgeTime;
            // float eased = t * t * t;
            float eased = (1f - t);         // linear deceleration
            // float eased = (1f - t) * (1f - t) * (1f - t);  // cubic ease-out

            // Determine the speed
            float baseSpeed = _stats.DodgeDist.Value / _stats.DodgeTime.Value;

            //Determine the direction
            Vector2 dir = ConvertDirection(dodgeState.DodgeDirection);
            Debug.Log($"Direction update result: {dir}");
            float a = 0.5f;
            float b = 2f;

            float speedFactor = a + b * eased;

            // currentResult.Velocity = baseSpeed * speedFactor * dir;
            currentResult.Velocity = t * 2 * baseSpeed * dir;

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