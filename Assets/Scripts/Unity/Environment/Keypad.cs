using System;
using System.Collections.Generic;
using Application.Environment;
using Core.Environment.Abstractions;
using Core.Environment.Interactions;
using Environment.Core.Interactions;
using Game.Application.UI.Menus.UICommands;
using Game.Core.Cameras;
using Game.Core.Execution;
using Game.Core.Interactions;
using Game.Unity.Events;
using Infrastructure.Unity.Registries;
using Primitives.Input;
using Primitives.Menus.Commands;
using Primitives.Unity.UI.Menus;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Environment
{
    public class Keypad : SelfRegister<IInitializable<IGameContext>>, IContextInteractable, IInitializable<IGameContext>, InteractableState, IPinValidator, IKeyEnterable, IKeypadPresenter
    {
        [SerializeField] private AudioFeedBack _successAudio;
        [SerializeField] private AudioFeedBack _failureAudio;
        private bool _isInUse = false;
        private bool _isSolved = false;
        [SerializeField] private SerializedInterface<ICameraProvider> _cameraProviderMono;
        private ICameraProvider _cameraProvider => _cameraProviderMono.Interface;

        [SerializeField] private MenuRouter _menuRouter;

        private IActiveCameraChanger _cameraChanger;
        private IInteractContext _currentContext = null;
        private IKeypadBrain _keypadBrain;
        [SerializeField] private SerializedInterface<IKeypadPresenter> _keypadPresenterMono;
        private IKeypadPresenter _keypadPresenter => _keypadPresenterMono.Interface;

        [SerializeField] private SerializedInterface<IDoor> _doorMono;
        private IDoor _door => _doorMono.Interface;

        [SerializeField] private int _priority;
        public int Priority => _priority;

        [Header("Debug")]
        [SerializeField] string _pin;

        private void Awake()
        {
            base.Awake();
            _keypadBrain = new KeypadBrain(6);
        }

        public bool CanInteract()
        {
            // If you've solved the puzzle stop touching this
            return !_isSolved;
        }

        public void Initialize(IGameContext context)
        {
            _cameraChanger = context.CameraService.CameraChanger;
        }
        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_cameraChanger != null, $"Unable to get camera changer service");
        }

        public void Interact(IInteractContext context)
        {
            Debug.Log($"Keypad interaction");
            if (CanInteract())
            {
                // cache the context
                _currentContext = context;
                StartInteraction();
            }
        }

        public void Interact() { }

        public void StartInteraction()
        {
            _isInUse = true;
            // Switch to the camera
            _cameraChanger.AddCamera(_cameraProvider.GetCamera());
            // Change the player's action map
            _currentContext.PlayerActionMapManager.SetActionMap(InputContext.Menu);
            // Sub the events
            _menuRouter.ConnectInputProvider(_currentContext.MenuInputReader);
            _menuRouter.OnCommand += HandleCommand;
            // Highlight the current selected index


        }

        public void EndInteraction()
        {
            _isInUse = false;
            // Switch to the camera
            _cameraChanger.RemoveCamera(_cameraProvider.GetCamera());
            // Change the player's action map
            _currentContext.PlayerActionMapManager.SetActionMap(InputContext.Gameplay);
            // Unsub the events
            _menuRouter.DisconnectInputProvider();
            _menuRouter.OnCommand -= HandleCommand;
            _currentContext = null;
        }

        public void GeneratePin(int length)
        {
            _keypadBrain.GeneratePin(length);
        }

        public bool IsValid()
        {
            bool res = _keypadBrain.IsValid();
            _isSolved = res;
            if (res)
            {
                // open the door
                _door.SetLocked(false);
                // play a sound
                _successAudio.React();
                // End the interaction
                EndInteraction();
                return res;
            }
            _failureAudio.React();
            return res;
        }

        public bool EnterValue(int val)
        {
            bool res = _keypadBrain.EnterValue(val);
            UpdateDisplay(_keypadBrain.CurrentEntry);
            return res;
        }

        public void UpdateDisplay(List<int> val) => _keypadPresenter.UpdateDisplay(val);
        public void ClearDisplay() => _keypadPresenter.ClearDisplay();

        public void RemoveLast()
        {
            _keypadBrain.RemoveLast();
            UpdateDisplay(_keypadBrain.CurrentEntry);
        }
        private void HandleCommand(IUICommand command)
        {
            if (command is BackCommand)
            {
                EndInteraction();
            }
        }
        private void LateUpdate()
        {
            _pin = "";
            for (int i = 0; i < _keypadBrain.Pin.Count; i++)
            {
                _pin += _keypadBrain.Pin[i].ToString();
            }
        }
    }
}