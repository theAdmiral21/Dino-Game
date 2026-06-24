using UnityEngine;
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;

using Movement.Core.Enums;
using Movement.Core.Rules;
using Movement.Core.Abstractions;
using Core.Movement.Inputs;

namespace Movement.Core.Movement
{
    /// <summary>
    /// This class is used to evaluate the following rules for stopping a run:
    /// - The player can stop only when grounded
    /// 
    /// </summary>
    public static class RunStopRules
    {
        public static RunStopResult TryStopRun(IActorInput inputs, PhysicsContext facts, object ruleState)
        {
            // Verify the rule state can be evaluated
            if (!ruleState.TryGet<ILandingState>(out var landing)) return Denied();
            // if (!ruleState.TryGet<IQuickStepState>(out var quickStepState)) return Denied();

            if (Mathf.Abs(inputs.Move.x) < 0.25f)
                if (facts.IsGrounded || facts.IsOnPlatform)
                {
                    return Approved(RunType.Run, ActionPhase.Continuous);
                }
                else
                {
                    landing.LandingStopRequested = true;
                    return Approved(RunType.Aerial, ActionPhase.Continuous);
                }

            return Denied();
        }


        private static RunStopResult Approved(RunType type, ActionPhase phase)
        {
            // Debug.Log($"Run stop approved {type}");
            return new RunStopResult(true, type, ActionPhase.Continuous);
        }
        private static RunStopResult Denied()
        {
            // Debug.Log("Run stop denied");
            return new RunStopResult(false, RunType.None, ActionPhase.Continuous);

        }
    }
}