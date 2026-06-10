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
    public class CalcDoggoDash : ICalcAction
    {
        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult)
        {
            stats.TryGet<DashStats>(out var _stats);
            if (actionResult is not DoggoDashResult dash) return currentResult;

            if (!dash.Approved) return currentResult;

            // Calculate the dash magnitude
            float dashSpeed = _stats.DashDist.Value / _stats.DashTime.Value;
            // Get the direction vector
            Vector2 dir = ConvertDirection(dash.Direction);
            // Set the direction
            currentResult.Velocity = dashSpeed * dir;

            Debug.Log($"Dash direction: {dir}");

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