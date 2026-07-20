using System;
using Infrastructure.Core.Inputs;
using PlayerController.Unity.Inputs;
using Primitives.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Unity.PlayerController.Inputs
{
    public class ConversationInputReader : BaseInputReader, GameInputs.IInConversationActions, IConversationInputReader
    {
        public override InputContext Type => InputContext.Conversation;
        public override bool IsActive => _actions.InConversation.enabled;

        public event Action OnCompleteDialogPressed;
        public event Action OnSelectDialogPressed;
        public event Action<Vector2> OnNavigatePressed;
        public override void Initialize(GameInputs inputActions)
        {
            _actions = inputActions;
            _actions.InConversation.SetCallbacks(this);
            // Debug.Log($"{this} has input action asset: {_actions}");
            Deactivate();
            // Debug.Log($"{name} is active: {IsActive}");
        }
        private void OnDestroy()
        {
            _actions.InConversation.Disable();
            _actions.InConversation.RemoveCallbacks(this);
        }
        public override void Activate() => _actions.InConversation.Enable();
        public override void Deactivate() => _actions.InConversation.Disable();
        public void OnCompleteDialog(InputAction.CallbackContext context)
        {
            Debug.Log($"Complete dialog received!");
            if (context.started)
                OnCompleteDialogPressed?.Invoke();
        }

        public void OnNavigate(InputAction.CallbackContext context)
        {
            Debug.Log($"Navigate dialog received!");
            if (context.started)
                OnNavigatePressed?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnSelectDialog(InputAction.CallbackContext context)
        {
            Debug.Log($"Select dialog received!");
            if (context.started)
                OnSelectDialogPressed?.Invoke();
        }


    }
}