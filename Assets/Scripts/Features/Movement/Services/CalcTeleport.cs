using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Stats;
using Movement.Features.Movement.Abstractions;
using Primitives.Physics;
using Primitives.Stats.DataStructures;
using UnityEngine;

namespace Movement.Features.Movement.Services
{
    public class CalcTeleport : ICalcAction
    {
        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult)
        {
            stats.TryGet<TeleportStats>(out var _stats);

            if (actionResult is not TeleportResult teleport || !actionResult.Approved) return currentResult;

            var dest = teleport.Destination * _stats.TeleportRange.Value;
            Debug.Log($"Calculated destination: {dest}");

            return currentResult;
        }
    }
}