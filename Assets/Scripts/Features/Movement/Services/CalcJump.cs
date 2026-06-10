using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Stats;
using Movement.Features.Movement.Abstractions;
using Primitives.Physics;
using Primitives.Stats.DataStructures;
using UnityEngine;

namespace Movement.Features.Movement.Services
{
    public class CalcJump : ICalcAction
    {
        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult)
        {
            stats.TryGet<JumpStats>(out var jumpStats);
            stats.TryGet<WallStats>(out var wallStats);
            stats.TryGet<DoubleJumpStats>(out var doubleJumpStats);
            stats.TryGet<LongJumpStats>(out var longJumpStats);
            stats.TryGet<QuickStepStats>(out var _quickStepStats);

            if (actionResult is not JumpResult jump || !actionResult.Approved) return currentResult;

            // Debug.Log($"Calculated jump type: {jump.Type}");

            // THIS IS WHAT NEEDS TO BE UPDATED.. maybe? 
            if (jump.Type == JumpType.WallJump)
            {
                float gravity = -2 * wallStats.WallJumpHeight.Value / Mathf.Pow(wallStats.WallJumpApexTime.Value, 2);
                currentResult.Gravity = gravity;
                currentResult.Velocity.y = Mathf.Abs(gravity) * wallStats.WallJumpApexTime.Value;
                currentResult.Velocity.x = -jump.Dir * (wallStats.WallJumpXDistance.Value / wallStats.WallJumpXDuration.Value);
                Debug.Log($"Wall jump velocity: {currentResult.Velocity} - Frame: {Time.frameCount}");
            }
            // TODO Figure out why this doesn't jump to the requested height
            else if (jump.Type == JumpType.Double)
            {
                // Calculate the double jump variables
                float gravity = -2 * doubleJumpStats.DoubleJumpHeight.Value / Mathf.Pow(doubleJumpStats.DoubleJumpApexTime.Value, 2);
                currentResult.Gravity = gravity;
                currentResult.Velocity.y = Mathf.Abs(gravity) * doubleJumpStats.DoubleJumpApexTime.Value;
            }
            else if (jump.Type == JumpType.LongJumpFar)
            {
                // Calculate the jump variables
                float gravity = -2 * jumpStats.JumpHeight.Value / Mathf.Pow(jumpStats.JumpApexTime.Value, 2);
                currentResult.Gravity = gravity;
                currentResult.Velocity.y = Mathf.Abs(gravity) * jumpStats.JumpApexTime.Value;
                currentResult.Velocity.x = longJumpStats.LongJumpFarSpeed.Value * jump.Dir;
                Debug.Log($"Long jump vel: {currentResult.Velocity}");
            }
            else if (jump.Type == JumpType.LongJumpMed)
            {
                // Calculate the jump variables
                float gravity = -2 * jumpStats.JumpHeight.Value / Mathf.Pow(jumpStats.JumpApexTime.Value, 2);
                currentResult.Gravity = gravity;
                currentResult.Velocity.y = Mathf.Abs(gravity) * jumpStats.JumpApexTime.Value;
                currentResult.Velocity.x = longJumpStats.LongJumpMedSpeed.Value * jump.Dir;
                Debug.Log($"Long jump vel: {currentResult.Velocity}");
            }
            else
            {
                // Calculate the jump variables
                float gravity = -2 * jumpStats.JumpHeight.Value / Mathf.Pow(jumpStats.JumpApexTime.Value, 2);
                currentResult.Gravity = gravity;
                currentResult.Velocity.y = Mathf.Abs(gravity) * jumpStats.JumpApexTime.Value;

                // Debug.Log($"Jump gravity: {gravity}; Jump velocity: {currentResult.Velocity.y}");
            }

            return currentResult;
        }
    }
}