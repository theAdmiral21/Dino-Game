
using Movement.Core.Movement.DataStructures;
using Movement.Core.Inputs;
using Primitives.Physics;
using Movement.Core.Abstractions;

namespace Movement.Core.Rules
{
    public static class StopZoomiesRules
    {
        public static StopZoomiesResult TryStopZoomies(PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            if (!ruleState.TryGet<IZoomiesState>(out var zoomies)) return Denied();

            if (zoomies.IsZooming) return Denied();
            return Approved();
        }

        private static StopZoomiesResult Approved()
        {
            return new StopZoomiesResult(true, Enums.ActionPhase.Override);
        }

        private static StopZoomiesResult Denied()
        {
            return new StopZoomiesResult(false, Enums.ActionPhase.Override);
        }

    }
}