using Movement.Core.Inputs;
using Movement.Core.Inputs.DataStructures;
using UnityEngine;

namespace PlayerController.Application.Inputs
{
    public class PlayerInputAdapter : IActorInput
    {
        public Vector2 Move => _inputState.LeftStick;
        public bool JumpPressed => _inputState.JumpPressed;
        public bool JumpHeld => _inputState.HoldingJump;
        private InputState _inputState;
        public PlayerInputAdapter(InputState inputState)
        {
            _inputState = inputState;
        }
    }
}