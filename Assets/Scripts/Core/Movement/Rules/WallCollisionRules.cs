using Movement.Core.Abstractions;
using Movement.Core.Enums;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Rules;
using Primitives.Physics;


namespace Movement.Core.Movement
{
    /// <summary>
    /// This class is used to evaluate the following rules for running:
    /// - Running into walls halts movement.
    /// 
    /// </summary>
    public static class WallCollisionRules
    {
        public static RunStopResult OnContact(PhysicsContext facts, object ruleState)
        {
            // Verify the rule state can be evaluated
            if (!ruleState.TryGet<IXInputState>(out var xInputState)) return Denied();
            if (!ruleState.TryGet<IDirectionState>(out var direction)) return Denied();

            if (facts.WallContactType == WallContact.None) return Denied();
            if (xInputState.XInputLocked) return Denied();
            // Basic checks
            if (direction.Dir == 1 && facts.WallContactType == WallContact.Right)
            {
                // Debug.Log($"[WallCollision] input direction: {direction.Dir}; Wall contact: {facts.WallContactType}");
                return Approved(RunType.Run);
            }

            if (direction.Dir == -1 && facts.WallContactType == WallContact.Left)
            {
                // Debug.Log($"[WallCollision] input direction: {direction.Dir}; Wall contact: {facts.WallContactType}");
                return Approved(RunType.Run);
            }

            return Denied();
        }

        private static RunStopResult Approved(RunType type)
        {
            // Debug.Log("Run stop approved");
            return new RunStopResult(true, type, ActionPhase.Continuous);
        }
        private static RunStopResult Denied()
        {
            // Debug.Log("Run stop denied");
            return new RunStopResult(false, RunType.None, ActionPhase.Continuous);

        }
    }
}