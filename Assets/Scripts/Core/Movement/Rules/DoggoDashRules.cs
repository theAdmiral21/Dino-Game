
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;
using Movement.Core.Abstractions;
using UnityEngine;
using Primitives.Input;
using Core.Movement.Inputs;

namespace Movement.Core.Rules
{
    public static class DoggoDashRules
    {
        /*
        Rules for activating a doggo dash
        - No dashing while already dashing
        - The dash amount must be greater than zero
        - The dash amount can not be negative
        - Dashing costs one dash amount
        - The direction input is normalized to one of eight directions
        */
        public static DoggoDashResult TryDoggoDash(DoggoDashRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            if (!ruleState.TryGet<IDisabledState>(out var disabledState)) return Denied();
            if (!ruleState.TryGet<IDodgeState>(out var doggoDash)) return Denied();
            // if (!ruleState.TryGet<ISwitchMovement>(out var switchMovement)) return Denied();

            // Evaluate the rules
            if (disabledState.IsDisabled) return Denied();
            // if (!switchMovement.DashMode) return Denied();

            // NOTE I may need to add a buffer window for dashing in case the player ever does have multiple dashes.
            // If a dash is available and we aren't currently dashing
            if (doggoDash.DodgeAmount > 0 && !doggoDash.IsDodging)
            {
                // Start the timer
                doggoDash.StartDodgeTimer();
                // Eat a dash
                doggoDash.DecrementDodge();
                // Determine the direction
                InputDirection dir = NormalizeInput(request.Direction);
                doggoDash.SetDodgeDirection(dir);
                return Approved(dir);
            }
            return Denied();
        }

        private static DoggoDashResult Approved(InputDirection dir)
        {
            // Debug.Log($"Dash approved");

            // Return the result
            return new DoggoDashResult(true, dir);
        }

        private static DoggoDashResult Denied()
        {
            // Debug.Log($"Dash denied");
            return new DoggoDashResult(false, InputDirection.Up);
        }

        private static InputDirection NormalizeInput(Vector2 dashDirection)
        {
            float sector = 2 * Mathf.PI / 8;
            int index = ((int)Mathf.Round(Mathf.Atan2(dashDirection.y, dashDirection.x) / sector) % 8 + 8) % 8;

            return index switch
            {
                0 => InputDirection.Right,
                1 => InputDirection.UpRight,
                2 => InputDirection.Up,
                3 => InputDirection.UpLeft,
                4 => InputDirection.Left,
                5 => InputDirection.DownLeft,
                6 => InputDirection.Down,
                7 => InputDirection.DownRight,
                _ => InputDirection.Right
            };
        }
    }
}