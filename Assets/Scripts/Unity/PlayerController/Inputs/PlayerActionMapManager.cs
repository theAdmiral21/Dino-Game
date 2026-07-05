using System;
using System.Collections.Generic;
using Primitives.Input;
using UnityEngine;
using Infrastructure.Unity.Registries;
using Game.Core.Execution;
using Primitives.GameState;
using Game.Core.State.Services;
using Infrastructure.Core.Inputs;

namespace PlayerController.Unity.Inputs
{
    public class PlayerActionMapManager : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>, IPlayerActionMapManager
    {
        [SerializeField] private bool _debugStatus;
        public bool IsEnabled { get; private set; }
        [SerializeField] private int _priority = 0;
        public int Priority => _priority;
        private GameInputs _actionAsset;
        private Dictionary<InputContext, BaseInputReader> _inputReaders = new();
        private IGameStateEvents _stateEvents;
        private IGameStateProvider _gameState;

        public event Action<InputContext> OnActionMapChanged;

        private void Awake()
        {
            // Register with the scene boot strapper in order initialize in the correct order
            base.Awake();

            _actionAsset = new GameInputs();
            BaseInputReader[] inputReaders = GetComponents<BaseInputReader>();
            // Debug.Log($"Found {inputReaders.Length} input readers");
            foreach (BaseInputReader reader in inputReaders)
            {
                reader.Initialize(_actionAsset);
                _inputReaders[reader.Type] = reader;
                // Debug.Log($"Set action asset for {reader.Type}");
            }
        }

        public void Initialize(IGameContext context)
        {
            _stateEvents = context.GameStateServices.GameStateEvents;
            _gameState = context.GameStateServices.GameState;
            _stateEvents.OnGameStateChanged += HandleStateChange;
        }
        public void PostInitialize(IGameContext context)
        {
            // After subscribing, make sure you're in the correct state
            HandleStateChange(_stateEvents.CurrentState);
        }

        public void EnableInputs()
        {
            HandleStateChange(_gameState.CurrentState);
            IsEnabled = true;
        }

        public void DisableInputs()
        {
            SetActionMap(InputContext.Disabled);
            IsEnabled = false;
        }

        private void OnDisable()
        {
            _stateEvents.OnGameStateChanged -= HandleStateChange;
        }
        private void HandleStateChange(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Booting:
                    {
                        SetActionMap(InputContext.Menu);
                        break;
                    }
                case GameState.Gameplay:
                    {
                        SetActionMap(InputContext.Gameplay);
                        break;
                    }
                case GameState.Paused:
                    {
                        SetActionMap(InputContext.Pause);
                        break;
                    }
            }
        }
        private void Update()
        {
            if (_debugStatus)
            {
                foreach (var reader in _inputReaders.Values)
                {
                    Debug.Log($"{reader.Type} is active: {reader.IsActive}");
                }
            }
        }

        private void DisableAll()
        {
            foreach (BaseInputReader reader in _inputReaders.Values)
            {
                reader.Deactivate();
            }
        }

        public void SetActionMap(InputContext inputContext)
        {
            Debug.Log($"Changing action map");
            DisableAll();
            switch (inputContext)
            {
                case InputContext.Gameplay:
                    {
                        _inputReaders[InputContext.Gameplay].Activate();
                        break;
                    }
                case InputContext.Menu:
                    {
                        _inputReaders[InputContext.Menu].Activate();
                        break;
                    }
                case InputContext.Conversation:
                    {
                        _inputReaders[InputContext.Conversation].Activate();
                        break;
                    }
                case InputContext.Pause:
                    {
                        _inputReaders[InputContext.Pause].Activate();
                        break;
                    }
                case InputContext.CutScene:
                    {
                        _inputReaders[InputContext.CutScene].Activate();
                        break;
                    }
                case InputContext.Disabled:
                    {
                        // Everything has already been disabled so if you request this do nothing.
                        break;
                    }
                case InputContext.None:
                    {
                        Debug.LogError($"{inputContext} was routed to None. Was that on purpose?");
                        break;
                    }
            }
        }
        [ContextMenu("Set Action Map / Gameplay")]
        private void DebugGameplay()
        {
            SetActionMap(InputContext.Gameplay);
            Debug.Log("Switched to Gameplay map");
        }

        [ContextMenu("Set Action Map / Menu")]
        private void DebugMenu()
        {
            SetActionMap(InputContext.Menu);
            Debug.Log("Switched to Menu map");
        }

        [ContextMenu("Set Action Map / Pause")]
        private void DebugPause()
        {
            SetActionMap(InputContext.Pause);
            Debug.Log("Switched to Pause map");
        }

        [ContextMenu("Set Action Map / Conversation")]
        private void DebugConversation()
        {
            SetActionMap(InputContext.Conversation);
            Debug.Log("Switched to Conversation map");
        }

        [ContextMenu("Set Action Map / Cut Scene")]
        private void DebugCutScene()
        {
            SetActionMap(InputContext.CutScene);
            Debug.Log("Switched to CutScene map");
        }


    }
}