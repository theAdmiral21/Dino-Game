using UnityEngine;
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;
using Movement.Core.Abstractions;
using Movement.Core.Rules;
using Movement.Core.Inputs;


namespace Movement.Core.Movement
{
    /// <summary>
    /// This class is used to evaluate the following rules for running:
    /// - The player can only run if the x input is not locked.
    /// - Running into walls halts movement.
    /// 
    /// </summary>
    public static class TeleportRules
    {
        public static TeleportResult TryTeleport(TeleportRequest request, PhysicsContext facts, IActorInput inputs, object ruleState)
        {
            // Verify the rule state can be evaluated
            if (!ruleState.TryGet<ITeleportState>(out var teleport)) return Denied();
            if (!ruleState.TryGet<IDisabledState>(out var disabledState)) return Denied();

            // if not request OR we're already stunned, deny
            if (!request.Requested || disabledState.IsDisabled) return Denied();

            if (teleport.CanTeleport)
            {
                teleport.StartTeleportCoolDown();
                return Approved(request.CurrentPosition, request.Destination);
            }
            return Denied();
        }

        private static TeleportResult Approved(Vector2 current, Vector2 dest)
        {
            Debug.Log("Teleport approved");
            return new TeleportResult(true, current, dest);
        }
        private static TeleportResult Denied()
        {
            Debug.Log("Teleport denied");
            return new TeleportResult(false, Vector2.one, Vector2.one);

        }
    }
}