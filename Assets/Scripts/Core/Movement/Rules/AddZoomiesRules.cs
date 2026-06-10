
using Movement.Core.Movement.DataStructures;
using Movement.Core.Inputs;
using Primitives.Physics;
using Movement.Core.Abstractions;

namespace Movement.Core.Rules
{
    public static class AddZoomiesRules
    {
        public static AddZoomiesResult TryAddZoomies(AddZoomiesRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            if (!ruleState.TryGet<IZoomiesState>(out var zoomies)) return Denied();
            // This is more of a formality because the player's rule state handles the actual permissions. However I didn't have a better idea for getting the amount to the rule state...
            zoomies.AddZoomies(request.Amount);

            return Approved();
        }

        private static AddZoomiesResult Approved()
        {
            return new AddZoomiesResult(true, Enums.ActionPhase.Override);
        }

        private static AddZoomiesResult Denied()
        {
            return new AddZoomiesResult(false, Enums.ActionPhase.Override);
        }

    }
}