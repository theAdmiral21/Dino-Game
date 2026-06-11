using System;
using UnityEngine.InputSystem;

namespace PlayerController.Features.Inputs.DataStructures
{
    /// <summary>
    /// Structure containing actions for sending inputs to where they belong
    /// </summary>
    public struct InputEvents
    {
        public Action<InputAction.CallbackContext> OnXInput;
        public Action<InputAction.CallbackContext> OnYInput;
        public Action<InputAction.CallbackContext> OnJump;
    }
}
