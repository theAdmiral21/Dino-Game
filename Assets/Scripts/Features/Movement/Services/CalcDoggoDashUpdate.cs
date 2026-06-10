using UnityEngine;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Stats;
using Movement.Features.Movement.Abstractions;
using Primitives.Physics;
using Primitives.Stats.DataStructures;
using Primitives.Input;
using System;

namespace Movement.Features.Movement.Services
{
    public class CalcDoggoDashUpdate : ICalcAction
    {
        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult)
        {
            stats.TryGet<DashStats>(out var _stats);
            if (actionResult is not DoggoDashUpdateResult dash) return currentResult;

            if (!dash.Approved) return currentResult;

            // Ease the dash
            var dashState = dash.DashState;

            float t = dashState.DashCounter / dashState.DashTime;
            // float eased = t * t * t;
            float eased = (1f - t);         // linear deceleration
            // float eased = (1f - t) * (1f - t) * (1f - t);  // cubic ease-out

            // Determine the speed
            float baseSpeed = _stats.DashDist.Value / _stats.DashTime.Value;

            //Determine the direction
            Vector2 dir = ConvertDirection(dashState.DashDirection);
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
            Debug.Log($"Dash input direction: {dash}");
            switch (dash)
            {
                case InputDirection.Up:
                    {
                        return Vector2.up;
                    }
                case InputDirection.UpRight:
                    {
                        return (Vector2.up + Vector2.right).normalized;
                    }
                case InputDirection.Right:
                    {
                        return Vector2.right;
                    }
                case InputDirection.DownRight:
                    {
                        return (Vector2.down + Vector2.right).normalized;
                    }
                case InputDirection.Down:
                    {
                        return Vector2.down;
                    }
                case InputDirection.DownLeft:
                    {
                        return (Vector2.down + Vector2.left).normalized;
                    }
                case InputDirection.Left:
                    {
                        return Vector2.left;
                    }
                case InputDirection.UpLeft:
                    {
                        return (Vector2.up + Vector2.left).normalized;
                    }
                default:
                    {
                        return Vector2.right;
                    }
            }
        }

    }
}