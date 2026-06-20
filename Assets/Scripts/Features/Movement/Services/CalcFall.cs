using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Stats;
using Movement.Features.Movement.Abstractions;
using Primitives.Physics;
using Primitives.Stats.DataStructures;
using UnityEngine;

namespace Movement.Features.Movement.Services
{
    public class CalcFall : ICalcAction
    {


        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult)
        {
            stats.TryGet<GravityStats>(out var gravStats);
            // stats.TryGet<WallStats>(out var wallStats);
            if (actionResult is not FallResult fall) return currentResult;
            // If we can't fall then don't
            if (!fall.Approved)
            {
                return currentResult;
            }
            else
            {
                // Debug.Log($"Got fall type: {fall.Type}");
                if (fall.Type == FallType.Fast)
                {
                    currentResult.Gravity = gravStats.BaseGravity.Value * gravStats.FastFall.Value;
                }
                else if (fall.Type == FallType.Slow)
                {
                    currentResult.Gravity = gravStats.BaseGravity.Value * gravStats.SlowFall.Value;
                }
                // else if (fall.Type == FallType.WallSlide)
                // {
                //     currentResult.Velocity.y = wallStats.WallSlideSpeed.Value;
                //     currentResult.Gravity = 0;
                // }
                else if (fall.Type == FallType.None)
                {
                    if (currentResult.Velocity.y < 0)
                    {
                        currentResult.Velocity.y = 0;
                    }
                    currentResult.Gravity = 0;
                }
            }
            // Debug.Log($"Fall type: {fall.Type} - Frame: {Time.frameCount}");
            // Debug.Log($"fall result: {currentResult.Velocity} - Frame: {Time.frameCount}");
            // Debug.Log($"Gravity: {currentResult.Gravity} - Frame: {Time.frameCount}");
            return currentResult;
        }

    }
}