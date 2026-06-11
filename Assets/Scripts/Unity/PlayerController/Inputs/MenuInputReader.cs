using System;
using Primitives.UI.Menus.Core.Enums;
using Primitives.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Primitives.Input.Enums;

namespace PlayerController.Unity.Inputs
{
    public class MenuInputReader : BaseInputReader, GameInputs.IInMenuActions
    {
        public override InputContext Type => InputContext.Menu;
        // Menu Actions
        public Vector2 Navigate => _navigate;
        private Vector2 _navigate;

        public event Action<UIInput> Select;
        public event Action<UIInput> Back;

        public InputProviderType CurrentProvider => _currentProvider;

        private InputProviderType _currentProvider;

        public float LastUsedTime => _lastUsedTime;
        private float _lastUsedTime;
        public override bool IsActive => _actions.InMenu.enabled;
        public override void Initialize(GameInputs inputActions)
        {
            _actions = inputActions;
            _actions.InMenu.SetCallbacks(this);
            // Debug.Log($"{this} has input action asset: {_actions}");
            Deactivate();
            // Debug.Log($"{name} is active: {IsActive}");
        }
        private void OnDestroy()
        {
            _actions.InMenu.Disable();
            _actions.InMenu.RemoveCallbacks(this);
        }
        public override void Activate() => _actions.InMenu.Enable();
        public override void Deactivate() => _actions.InMenu.Disable();
        public void OnNavigate(InputAction.CallbackContext context)
        {
            GetDeviceType(context);
            Debug.Log("Sent navigate input");
            _navigate = context.ReadValue<Vector2>();
        }

        public void OnSelect(InputAction.CallbackContext context)
        {
            GetDeviceType(context);
            if (context.started)
            {
                Debug.Log("Sent select input");
                Select?.Invoke(UIInput.Select);
            }
        }
        public void OnBack(InputAction.CallbackContext context)
        {
            GetDeviceType(context);
            if (context.started)
            {
                Debug.Log("Sent back input");
                Back?.Invoke(UIInput.Back);
            }
        }
        private InputProviderType GetDeviceType(InputAction.CallbackContext context)
        {
            InputDevice device = context.control.device;
            InputProviderType type = device is Gamepad ? InputProviderType.Gamepad : InputProviderType.Keyboard;
            _currentProvider = type;
            _lastUsedTime = Time.unscaledTime;
            return type;
        }
    }
}