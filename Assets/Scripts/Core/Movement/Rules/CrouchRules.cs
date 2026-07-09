
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;
using Movement.Core.Abstractions;
using UnityEngine;
using Core.Movement.Inputs;

namespace Movement.Core.Rules
{
    public static class CrouchRules
    {
        public static CrouchResult TryCrouch(CrouchRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            if (!ruleState.TryGet<ICrouchState>(out var crouchState)) return Denied();

            // if you arent crouching, and you're grounded, go ahead and crouch
            if (!crouchState.IsCrouching)
            {
                if (facts.IsGrounded || facts.IsOnPlatform)
                {
                    crouchState.SetCrouchState(true);
                    return Approved(true);
                }
            }
            else
            // if you are crouching, check if you can stand before standing up
            {
                if (request.CanStand)
                {
                    crouchState.SetCrouchState(false);
                    return Approved(false);
                }
            }


            //     if (crouchState.IsCrouching && !request.CanStand)
            //     {
            //         return Denied();
            //     }
            //     bool newCrouchValue = !crouchState.IsCrouching;
            //     crouchState.SetCrouchState(newCrouchValue);
            //     return Approved(newCrouchValue);
            // }
            // crouchState.SetCrouchState(false);
            return Denied();
        }

        private static CrouchResult Approved(bool newCrouchValue)
        {
            Debug.Log($"Approved crouch");
            return new CrouchResult(true, newCrouchValue);
        }

        private static CrouchResult Denied()
        {
            Debug.Log($"Denied crouch");
            return new CrouchResult(false, false);
        }

    }
}