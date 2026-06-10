
using Movement.Core.Movement.DataStructures;
using Movement.Core.Inputs;
using Primitives.Physics;
using Movement.Core.Abstractions;
using UnityEngine;

namespace Movement.Core.Rules
{
    public static class ZoomiesRules
    {
        public static ZoomiesResult TryZoomies(ZoomiesRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            if (!ruleState.TryGet<IZoomiesState>(out var zoomies)) return Denied();

            if (zoomies.ZoomyAmount > zoomies.MinimumRequiredZoom && !zoomies.IsZooming)
            {
                zoomies.StartZoomiesTimer();
                return Approved();
            }
            return Denied();

        }

        private static ZoomiesResult Approved()
        {
            Debug.Log("Zoom approved");
            return new ZoomiesResult(true, Enums.ActionPhase.Override);
        }

        private static ZoomiesResult Denied()
        {
            Debug.Log("Zoom denied");
            return new ZoomiesResult(false, Enums.ActionPhase.Override);
        }

    }
}