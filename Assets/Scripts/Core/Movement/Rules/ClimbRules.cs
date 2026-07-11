
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;
using Core.Movement.Inputs;
using UnityEngine;
using Movement.Core.Abstractions;
using Primitives.Physics.Enums;

namespace Movement.Core.Rules
{
    public static class ClimbRules
    {
        public static ClimbResult TryClimb(ClimbRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            if (!ruleState.TryGet<IDisabledState>(out var disabledState)) return Denied();
            if (!ruleState.TryGet<IStunState>(out var stunState)) return Denied();
            if (!ruleState.TryGet<IClimbState>(out var climbState)) return Denied();


            if (disabledState.IsDisabled || stunState.IsStunned) return Denied();

            Debug.Log($"Requesting climbType: {request.Climb}");
            climbState.SetClimbingSurface(request.Climb);
            if (request.Climb != ClimbType.None)
            {
                // if you aren't climbing
                if (!climbState.IsClimbing)
                {
                    // if you want to go down and you're at the top of a climbing surface
                    if ((request.Climb == ClimbType.LadderTop || request.Climb == ClimbType.StairsTop) && request.InputDir.y < 0)
                    {
                        climbState.SetClimbing(true);
                        return Approved(request);
                    }

                    // if you want to go up and you're at the bottom of a climbing surface
                    if ((request.Climb == ClimbType.LadderBottom || request.Climb == ClimbType.StairsBottom) && request.InputDir.y > 0)
                    {
                        climbState.SetClimbing(true);
                        // climbState.SetClimbingSurface(request.Climb);
                        return Approved(request);
                    }
                }
                // If you're already climbing something
                else
                {
                    // if you're not touching the ground
                    if (request.Climb != ClimbType.None && !facts.IsGrounded && !facts.IsOnPlatform)
                    {
                        // keep climbing
                        return Approved(request);
                    }
                }
            }

            // if neither of the above are true
            climbState.SetClimbing(false);
            // climbState.SetClimbingSurface(ClimbType.None);
            return Denied();
        }

        private static ClimbResult Approved(ClimbRequest request)
        {
            Debug.Log("Climb approved");
            return new ClimbResult(true, request.InputDir, request.Climb);
        }

        private static ClimbResult Denied()
        {
            Debug.Log("Climb denied");
            // Return a result that makes no sense and is denied
            return new ClimbResult(false, Vector2.zero, ClimbType.LadderBottom);
        }

    }
}