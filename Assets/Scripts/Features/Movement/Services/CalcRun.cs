using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Stats;
using Movement.Features.Movement.Abstractions;
using Primitives.Physics;
using Primitives.Stats.DataStructures;
using UnityEngine;

namespace Movement.Features.Movement.Services
{
    public class CalcRun : ICalcAction
    {

        public CalcRun()
        {

        }

        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult)
        {
            // Stats = stats;
            stats.TryGet<RunStats>(out RunStats _runStats);
            stats.TryGet<SprintStats>(out SprintStats _sprintStats);
            stats.TryGet<AerialStats>(out AerialStats _aerialStats);
            stats.TryGet<ZoomiesStats>(out ZoomiesStats zoomStats);

            if (actionResult is not RunResult run || !actionResult.Approved) return currentResult;

            float target = 0;
            float accelValue = 0;
            float dir = run.Value.x >= 0 ? 1f : -1f;
            float vDir = currentResult.Velocity.x >= 0 ? 1f : -1f;

            float xInput = run.Value.x;
            if (run.Type == RunType.Run)
            {
                // Calculate the run speed
                target = _runStats.RunSpeed.Value * xInput;
                if (dir == vDir)
                {
                    // if (Mathf.Abs(target) < Mathf.Abs(currentResult.Velocity.x)) target = currentResult.Velocity.x;
                    accelValue = _runStats.RunAccel.Value;
                }
                else
                {
                    accelValue = _runStats.BrakeAccel.Value;
                }
            }
            else if (run.Type == RunType.Aerial)
            {
                // Calculate Aerial movement speed
                target = _runStats.RunSpeed.Value * xInput;
                if (dir == vDir)
                {
                    if (Mathf.Abs(target) < Mathf.Abs(currentResult.Velocity.x)) target = currentResult.Velocity.x;

                    accelValue = _aerialStats.AerialAccel.Value;
                }
                else
                {
                    accelValue = _aerialStats.AerialBrake.Value;
                }
            }
            else if (run.Type == RunType.Sprint)
            {
                // Gotta go fast!
                target = _sprintStats.SprintSpeed.Value * xInput;

                if (dir == vDir)
                {
                    if (Mathf.Abs(target) < Mathf.Abs(currentResult.Velocity.x)) target = currentResult.Velocity.x; accelValue = _sprintStats.SprintAccel.Value;
                }
                else
                {
                    accelValue = _runStats.BrakeAccel.Value;
                }
            }
            else if (run.Type == RunType.CrouchWalk)
            {
                // Calculate the run speed
                target = _runStats.CrouchWalkSpeed.Value * xInput;
                if (dir == vDir)
                {
                    accelValue = _runStats.CrouchWalkAccel.Value;
                }
                else
                {
                    accelValue = _runStats.CrouchWalkBrake.Value;
                }
            }

            // Smooth things out
            // Debug.Log($"accel value: {accelValue}");
            currentResult.Velocity.x = Mathf.MoveTowards(currentResult.Velocity.x, target, accelValue * currentResult.Dt);
            // currentResult.Velocity.x = target;

            return currentResult;
        }
    }
}