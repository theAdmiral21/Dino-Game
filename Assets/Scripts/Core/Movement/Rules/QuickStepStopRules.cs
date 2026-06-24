using Core.Movement.Inputs;
using Movement.Core.Abstractions;
using Movement.Core.Enums;
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;
using UnityEngine;

namespace Movement.Core.Rules
{
    public static class QuickStepStopRules
    {
        public static QuickStepStopResult TryQuickStepStop(IActorInput inputs,
                                                            PhysicsContext facts,
                                                            object ruleState)
        {
            if (!ruleState.TryGet<IQuickStepState>(out var quickStepState)) return Denied();
            if (!inputs.TryGet<IPlayerInputs>(out var playerInputs)) return Denied();

            if (quickStepState.QuickSteppingLastFrame && !quickStepState.QuickStepActive)// && (facts.IsGrounded || facts.IsOnPlatform))
            {
                return Approved(playerInputs);
            }
            return Denied();
        }

        private static QuickStepStopResult Approved(IPlayerInputs input)
        {
            // Debug.Log($"Quick step stop approved");
            return new QuickStepStopResult(true, input.Move, input.SprintPressed, ActionPhase.Impulse);
        }
        private static QuickStepStopResult Denied()
        {
            // Debug.Log($"Quick step stop denied");
            return new QuickStepStopResult(false, Vector2.zero, false, ActionPhase.Impulse);

        }
    }
}