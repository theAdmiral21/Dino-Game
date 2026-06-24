
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;
using Movement.Core.Abstractions;
using Primitives.Input;
using UnityEngine;
using Core.Movement.Inputs;

namespace Movement.Core.Rules
{
    public static class DodgeRules
    {
        public static DodgeResult TryDodge(DodgeRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            if (!ruleState.TryGet<IDisabledState>(out var disabledState)) return Denied();
            if (!ruleState.TryGet<IDodgeState>(out var dodge)) return Denied();
            if (!ruleState.TryGet<IDirectionState>(out var directionState)) return Denied();
            if (!ruleState.TryGet<IInvincibleState>(out var invincibleState)) return Denied();

            Debug.Log($"Not disabled");
            // Evaluate the rules
            if (disabledState.IsDisabled) return Denied();
            if (!facts.IsGrounded && !facts.IsOnPlatform) return Denied();

            // If a dash is available and we aren't currently dashing
            Debug.Log($"dodge amount: {dodge.DodgeAmount}; is not dodging: {!dodge.IsDodging}");
            if (dodge.DodgeAmount > 0 && !dodge.IsDodging)
            {
                // Start the timer
                dodge.StartDodgeTimer();
                // Eat a dodge
                dodge.DecrementDodge();
                // Determine the direction
                InputDirection dir = NormalizeInput(request.Direction, directionState.Dir);
                dodge.SetDodgeDirection(dir);

                // Update invincibility
                invincibleState.UpdateDodgeInvincibility();

                return Approved(dir);
            }
            return Denied();
        }

        private static DodgeResult Approved(InputDirection dir)
        {
            // Return the result
            Debug.Log($"Dodge approved");
            return new DodgeResult(true, dir);
        }

        private static DodgeResult Denied()
        {
            Debug.Log($"Dodge denied");
            return new DodgeResult(false, InputDirection.Up);
        }

        private static InputDirection NormalizeInput(Vector2 dashDirection, float facingDirection)
        {
            if (dashDirection.x < 0) return InputDirection.Left;
            if (dashDirection.x > 0) return InputDirection.Right;

            if (facingDirection == 1) return InputDirection.Right;
            return InputDirection.Left;

            // float sector = 2 * Mathf.PI / 8;
            // int index = ((int)Mathf.Round(Mathf.Atan2(dashDirection.y, dashDirection.x) / sector) % 8 + 8) % 8;

            // return index switch
            // {
            //     0 => InputDirection.Right,
            //     1 => InputDirection.Right,
            //     3 => InputDirection.Left,
            //     4 => InputDirection.Left,
            //     5 => InputDirection.Left,
            //     7 => InputDirection.Right,
            //     _ => InputDirection.Right
            // };
        }

    }
}