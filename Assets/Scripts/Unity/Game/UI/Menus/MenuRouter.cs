using System;
using Primitives.UI.Menus.Unity.InputProvider;
using UnityEngine;
using Game.Application.UI.Menus.Abstractions;
using Game.Application.UI.Menus.UICommands;
using Primitives.Menus.Commands;
using Game.Core.UI.Menus.Abstractions;
using Game.Core.Execution;
using Primitives.Input.Enums;
using Infrastructure.Unity.Registries;
using Game.Core.Inputs;

namespace Primitives.Unity.UI.Menus
{
    public class MenuRouter : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>
    {
        // Input Provider
        private IUIInputProvider _inputProvider;
        [SerializeField] private MonoBehaviour inputMono;

        // Button debounce
        InputGate _selectGate = new InputGate(.15f);
        InputGate _backGate = new InputGate(.15f);

        // Debounce behavior
        private Vector2 _lastDir;
        private float _nextAllowed;
        private const float INITIAL_DELAY = 0.25F;
        private const float REPEAT_DELAY = 0.15f;

        public IUICommandHandler CommandHandler => _commandHandler;

        [SerializeField] private int _priority = 0;
        public int Priority => _priority;

        private IUICommandHandler _commandHandler;

        public event Action<IUICommand> OnCommand;

        private void Awake()
        {
            base.Awake();
            _inputProvider = inputMono as IUIInputProvider;
            if (_inputProvider == null)
            {
                Debug.LogError($"{name} has no input provider!");
            }
        }

        public void Initialize(IGameContext context)
        {
            if (_inputProvider != null) ConnectInputProvider(_inputProvider);
        }

        public void PostInitialize(IGameContext context)
        {
            // throw new NotImplementedException();
        }

        public void ConnectInputProvider(IUIInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
            _inputProvider.Select += DebounceInput;
            _inputProvider.Back += DebounceInput;
        }
        public void DisconnectInputProvider()
        {
            if (_inputProvider != null)
            {
                _inputProvider.Select -= DebounceInput;
                _inputProvider.Back -= DebounceInput;
                _inputProvider = null;
            }
        }
        private void OnDestroy()
        {
            DisconnectInputProvider();
            base.OnDestroy();
        }

        private void Update()
        {
            if (_inputProvider == null) return;
            // Debounce analog inputs
            AnalogDebounce();

        }
        private void AnalogDebounce()
        {
            Debug.Log($"Input dir: {_inputProvider.Navigate}");
            Vector2 dir = _inputProvider.Navigate;
            // If the magnitude of the input is small ignore it
            if (dir.magnitude < 0.5f)
            {
                _nextAllowed = 0;
                return;
            }
            // Get the analog input
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                dir.y = 0;
            }
            else
            {
                dir.x = 0;
            }
            Vector2 discrete = new Vector2(
                Math.Sign(dir.x),
                Math.Sign(dir.y)
            );
            // Apply debounce
            if (discrete == _lastDir && Time.unscaledTime < _nextAllowed) return;

            // Map the analog input to a ui input type
            UIInput input = MapAnalogStick(discrete);
            if (input != UIInput.None)
            {
                Debug.Log($"Emitting dir: {input}");
                Emit(new LocalNavigationCommand(input));
            }

            // set the next debounce time
            _nextAllowed = Time.unscaledTime + (_lastDir == Vector2.zero ? INITIAL_DELAY : REPEAT_DELAY);

            // Update the last direction
            _lastDir = discrete;
        }
        private void DebounceInput(UIInput input)
        {
            if (input == UIInput.Select && _selectGate.Allow())
            {
                Emit(new SubmitCommand());
            }
            else if (input == UIInput.Back && _backGate.Allow())
            {
                Emit(new BackCommand());
            }
        }

        private UIInput MapAnalogStick(Vector2 discrete)
        {
            if (discrete.x != 0)
            {
                if (discrete.x == 1)
                {
                    return UIInput.Right;
                }

                if (discrete.x == -1)
                {
                    return UIInput.Left;
                }
            }
            else
            {
                if (discrete.y == 1)
                {
                    return UIInput.Up;
                }

                if (discrete.y == -1)
                {
                    return UIInput.Down;
                }
            }
            return UIInput.None;
        }

        private void Emit(IUICommand command)
        {
            OnCommand?.Invoke(command);
        }


    }
}