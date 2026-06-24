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
    /// This class is used to evaluate the following rules for running:
    /// - The player can only run if the x input is not locked.
    /// - Running into walls halts movement.
    /// 
    /// </summary>
    public static class RunRules
    {


        public static RunResult TryRun(RunRequest request, PhysicsContext facts, IActorInput inputs, object ruleState)
        {
            // Verify the rule state can be evaluated
            if (!ruleState.TryGet<IDisabledState>(out var disabledState)) return Denied();
            // Debug.Log($"Passed disable check");
            if (!ruleState.TryGet<IStunState>(out var stunState)) return Denied();
            // Debug.Log($"Passed stun check");
            if (!ruleState.TryGet<IXInputState>(out var xInputState)) return Denied();
            // Debug.Log($"Passed xinput check");
            if (!ruleState.TryGet<IDirectionState>(out var dirState)) return Denied();
            // Debug.Log($"Passed direction check");
            if (!ruleState.TryGet<ILandingState>(out var landingState)) return Denied();
            // Debug.Log($"Passed landing check");


            // Basic checks
            if (!request.Requested || disabledState.IsDisabled || stunState.IsStunned) return Denied();
            // If the player is wall jumping, block x input
            if (xInputState.XInputLocked) return Denied();

            dirState.SetDirection(inputs, facts);

            float inputDir = Mathf.Sign(inputs.Move.x);

            if (inputDir == 1 && facts.WallContactType == WallContact.Right)
            {
                // Debug.Log($"[Run rules] input direction: {inputDir}; Wall contact: {facts.WallContactType}");
                return Denied();
            }

            if (inputDir == -1 && facts.WallContactType == WallContact.Left)
            {
                // Debug.Log($"[Run rules] input direction: {inputDir}; Wall contact: {facts.WallContactType}");
                return Denied();
            }

            if (facts.IsGrounded || facts.IsOnPlatform)
            {
                landingState.LandingStopRequested = false;

                if (inputs.TryGet<IPlayerInputs>(out var actorInput))
                {
                    if (actorInput.SprintPressed)
                    {
                        return Approved(RunType.Sprint, request.Value);
                    }
                }
                if (ruleState.TryGet<ICrouchState>(out var crouch))
                {
                    if (crouch.IsCrouching)
                    {
                        return Approved(RunType.CrouchWalk, request.Value);
                    }
                }

                return Approved(RunType.Run, request.Value);
            }

            if (!facts.IsGrounded || !facts.IsOnPlatform)
            {
                landingState.LandingStopRequested = false;
                if (inputs.TryGet<IPlayerInputs>(out var actorInput))
                {
                    if (actorInput.SprintPressed)
                    {
                        return Approved(RunType.Sprint, request.Value);
                    }
                }
                return Approved(RunType.Aerial, request.Value);
            }

            // Debug.Log($"Nothing passed");
            return Denied();
        }

        private static RunResult Approved(RunType type, Vector2 value)
        {
            // Debug.Log("Run approved");
            return new RunResult(true, value, type, ActionPhase.Continuous);
        }
        private static RunResult Denied()
        {
            // Debug.Log("Run denied");
            return new RunResult(false, Vector2.zero, RunType.None, ActionPhase.Continuous);

        }
    }
}