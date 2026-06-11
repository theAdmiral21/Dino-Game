using System;
using Primitives.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Primitives.Input.Enums;
using Game.Core.Inputs;

namespace PlayerController.Unity.Inputs
{
    public class PauseInputReader : BaseInputReader, GameInputs.IInPauseActions, IUIInputProvider
    {
        public override InputContext Type => InputContext.Pause;
        public override bool IsActive => _actions.InPause.enabled;

        public Vector2 Navigate => throw new NotImplementedException();

        public float LastUsedTime => throw new NotImplementedException();

        public event Action<UIInput> Select;
        public event Action<UIInput> Back;

        public override void Initialize(GameInputs inputActions)
        {
            _actions = inputActions;
            _actions.InPause.SetCallbacks(this);
            Deactivate();
        }
        private void OnDestroy()
        {
            _actions.InPause.Disable();
            _actions.InPause.RemoveCallbacks(this);
        }
        public override void Activate() => _actions.InPause.Enable();
        public override void Deactivate() => _actions.InPause.Disable();
        public void OnBack(InputAction.CallbackContext context)
        {
            Debug.Log($"Pause back received!");
        }

        public void OnNavigate(InputAction.CallbackContext context)
        {
            Debug.Log($"Pause navigate received!");

        }

        public void OnResumeGame(InputAction.CallbackContext context)
        {
            Debug.Log($"Resume received!");

        }

        public void OnSelect(InputAction.CallbackContext context)
        {
            Debug.Log($"Pause select received!");

        }




    }
}