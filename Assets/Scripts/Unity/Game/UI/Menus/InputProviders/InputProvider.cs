using UnityEngine;
using System;
using UnityEngine.InputSystem;
using Game.UI.Menus.Core.Enums;
using Game.Core.UI.Menus.Abstractions;
using Primitives.Input.Enums;

namespace Primitives.UI.Menus.Unity.InputProviders
{
    public class InputProvider : MonoBehaviour, IUIInputProvider, MenuInputProvider.IMenuActionsActions
    {
        public Vector2 Navigate => _navigate;
        private Vector2 _navigate;

        // Menu Actions
        public event Action<UIInput> Select;
        public event Action<UIInput> Back;

        public InputProviderType CurrentProvider => _currentProvider;

        private InputProviderType _currentProvider;

        public float LastUsedTime => _lastUsedTime;
        private float _lastUsedTime;

        // Input actions
        private MenuInputProvider _actions;

        private void Awake()
        {
            _actions = new MenuInputProvider();
            _actions.MenuActions.SetCallbacks(this);
        }
        private void OnEnable()
        {
            _actions.Enable();
        }

        private void OnDisable()
        {
            _actions.Disable();
        }

        public void OnNavigate(InputAction.CallbackContext context)
        {
            GetDeviceType(context);
            // Debug.Log("Sent navigate input");
            _navigate = context.ReadValue<Vector2>();
        }

        public void OnSelect(InputAction.CallbackContext context)
        {
            GetDeviceType(context);
            if (context.started)
            {
                // Debug.Log("Sent select input");
                Select?.Invoke(UIInput.Select);
            }
        }
        public void OnBack(InputAction.CallbackContext context)
        {
            GetDeviceType(context);
            if (context.started)
            {
                // Debug.Log("Sent back input");
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