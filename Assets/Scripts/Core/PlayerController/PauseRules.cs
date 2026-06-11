using PlayerController.Core.Movement.Abstractions;
using UnityEngine;
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;
using Movement.Core.Enums;
using Primitives.GameState;
using Movement.Core.Inputs;

namespace PlayerController.Core.Movement
{
    /// <summary>
    /// This class is used to evaluate the following rules for pausing:
    /// - Can only pause while in game
    /// </summary>
    public static class PauseRules
    {
        public static PauseResult TryPause(PauseRequest request, PhysicsContext facts, GameState gameState, IActorInput inputValues, object ruleState)
        {
            if (gameState == GameState.Gameplay)
            {
                return Approved();
            }

            return Denied();
        }

        private static PauseResult Approved()
        {
            Debug.Log("pause approved");
            return new PauseResult(true, ActionPhase.Override);
        }
        private static PauseResult Denied()
        {
            Debug.Log("pause denied");
            return new PauseResult(false, ActionPhase.Override);

        }
    }
}