using System;
using Movement.Core.Abstractions;
using Movement.Core.Movement.DataStructures;
using Primitives.Input;
using Unity.Common.Unity;
using UnityEngine;
using UnityEngine.InputSystem;
using Primitives.Physics;
using Core.Movement.Inputs;
using PlayerController.Core.Movement.DataStructures;
using System.Net.Mime;

/*
Optional actions to add later:
- air dodge
- quickstep
- pounce
- smash style dash
- fast fall
*/

namespace PlayerController.Unity.Inputs
{
    public class PlayerInputReader : BaseInputReader, GameInputs.IInGameActions, IPlayerInputs
    {
        [SerializeField] private SerializedInterface<IActionRequestSink> _requestHandlerMono;
        private IActionRequestSink _requestHandler => _requestHandlerMono.Interface;

        public override InputContext Type => InputContext.Gameplay;
        public Vector2 MoveInput => _moveInput;
        private Vector2 _moveInput = Vector2.zero;

        // Properties for building the current input state

        public Vector2 Move => _leftStick;
        private Vector2 _leftStick => _actions.InGame.XInput.ReadValue<Vector2>();
        public bool SprintPressed => _sprintPressed;
        private bool _sprintPressed =>
        (_actions.InGame.Sprint.phase == InputActionPhase.Started) ||
        (_actions.InGame.Sprint.phase == InputActionPhase.Performed);
        public bool RaiseWeapon => _raiseWeaponPressed;
        private bool _raiseWeaponPressed =>
                                    (_actions.InGame.RaiseWeapon.phase == InputActionPhase.Started) ||
                                    (_actions.InGame.RaiseWeapon.phase == InputActionPhase.Performed);
        public bool DodgePressed => _dodgePressed;
        private bool _dodgePressed => _actions.InGame.Dodge.phase == InputActionPhase.Started;
        public bool DodgeHeld => _dodgeHeld;
        private bool _dodgeHeld => _actions.InGame.Dodge.phase == InputActionPhase.Performed;
        public bool JumpPressed => _jumpPressed;
        private bool _jumpPressed => _actions.InGame.Jump.phase == InputActionPhase.Started;
        public bool JumpHeld => _holdingJump;
        private bool _holdingJump => _actions.InGame.Jump.phase == InputActionPhase.Performed;
        public bool CrouchPressed => _crouchPressed;
        private bool _crouchPressed => _actions.InGame.Crouch.phase == InputActionPhase.Started;





        // Event actions
        // public event Action<IActionRequest> JumpCancel;
        // public event Action<IActionRequest> Run;
        // public event Action<IActionRequest> RunStop;
        // public event Action<IActionRequest> QuickStep;
        // public event Action<IActionRequest> Teleport;
        // public event Action<IActionRequest> Bark;
        // public event Action<IActionRequest> Pause;
        // public event Action<IActionRequest> Scent;

        // Buffered events
        public event Action<IActionRequest> BufferJump;

        // Input timers
        const float DOUBLE_TAP_WINDOW = 0.25f;
        private float _prevTap;
        private float _lastTapTime;

        public override bool IsActive => _actions.InGame.enabled;


        public override void Initialize(GameInputs inputActions)
        {
            _actions = inputActions;
            _actions.InGame.SetCallbacks(this);
            // Debug.Log($"{this} has input action asset: {_actions}");
            Deactivate();
            // Debug.Log($"{name} is active: {IsActive}");
        }

        private void OnDestroy()
        {
            _actions.InGame.Disable();
            _actions.InGame.RemoveCallbacks(this);
        }

        public override void Activate()
        {
            _actions.InGame.Enable();
            _lastTapTime = 0;
            // Debug.Log($"In game actions enabled - Frame: {Time.frameCount}");
        }
        public override void Deactivate()
        {
            _actions.InGame.Disable();
            // Debug.Log($"In game actions disabled - Frame: {Time.frameCount}");
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            // Debug.Log($"Jump pressed");
            if (context.started)
            {
                // Debug.Log($"Recived jump input - Frame: {Time.frameCount}");
                // BufferJump?.Invoke(new JumpRequest(true));
                _requestHandler.EnqueueActionRequest(new JumpRequest(true, JumpType.Ground));
            }
            else if (context.canceled)
            {
                // Debug.Log($"Recived jump cancel input");
                // JumpCancel?.Invoke(new JumpCancelRequest { Requested = true });
                _requestHandler.EnqueueActionRequest(new JumpCancelRequest(true));
            }
        }
        public void OnXInput(InputAction.CallbackContext context)
        {
            Debug.Log($"Got x input");
            if (context.started)
            {
                float climbDir = Mathf.Sign(context.ReadValue<Vector2>().y);
                if (Mathf.Abs(climbDir) < 0.5f) return;

                // Evaluate if you can climb

            }
            else if (context.performed)
            {
                Debug.Log($"Received run input");
                OnMove(context);
            }
            else if (context.canceled)
            {
                OnStopMove(context);
            }
        }
        private void OnMove(InputAction.CallbackContext context)
        {
            if (context.ReadValue<Vector2>() != _moveInput)
            {
                _moveInput = context.ReadValue<Vector2>();
            }
        }
        private void OnStopMove(InputAction.CallbackContext context)
        {
            // Debug.Log($"Received run stop input");
            // RunStop?.Invoke(new RunStopRequest(true, context.ReadValue<Vector2>()));
            _requestHandler.EnqueueActionRequest(new RunStopRequest(true, context.ReadValue<Vector2>()));
            // _moveInput.x = 0;
            _moveInput = context.ReadValue<Vector2>();
        }

        private void Update()
        {
            if (MoveInput.x != 0)
            {
                // Run?.Invoke(new RunRequest(true, MoveInput));
                _requestHandler.EnqueueActionRequest(new RunRequest(false, MoveInput));
            }
            if (MoveInput.y != 0)
            {
                // Run?.Invoke(new RunRequest(true, MoveInput));
                _requestHandler.EnqueueActionRequest(new ClimbRequest(MoveInput));
            }
        }
        public void OnDodge(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Debug.Log($"Emitted dodge request - frame {Time.frameCount}");
                // Bark?.Invoke(new BarkRequest(true));
                _requestHandler.EnqueueActionRequest(new DodgeRequest());
            }

        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Debug.Log($"Crouch requested");
                _requestHandler.EnqueueActionRequest(new CrouchRequest());
            }
        }
        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Debug.Log("Pause requested");
                // Pause?.Invoke(new PauseRequest());
                _requestHandler.EnqueueActionRequest(new PauseRequest());
            }
        }
        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _requestHandler.EnqueueActionRequest(new InteractRequest());
            }
        }
        // private Vector2 GetPosition()
        // {
        //     if (transform.parent == null)
        //     {
        //         return transform.position;
        //     }
        //     else
        //     {
        //         return transform.parent.position;
        //     }
        // }

        public void OnReload(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                // Debug.Log($"Reload weapon requested");
                // _requestHandler.EnqueueActionRequest(new ReloadRequest());
            }
        }

        public void OnRaiseWeapon(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                // Debug.Log($"Raise weapon requested");
                _requestHandler.EnqueueActionRequest(new RaiseWeaponRequest(true));
            }
            else if (context.canceled)
            {
                // Debug.Log($"Lower weapon requested");
                _requestHandler.EnqueueActionRequest(new RaiseWeaponRequest(false));
            }

        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                // Debug.Log($"Shoot weapon requested");
                _requestHandler.EnqueueActionRequest(new ShootRequest());
            }
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            Vector2 pos = context.ReadValue<Vector2>();
            // Debug.Log($"Mouse position: {pos}");
            _requestHandler.EnqueueActionRequest(new AimRequest(pos));
        }

        public void OnSelectNextItem(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Debug.Log($"Enqueueing index next request");
                _requestHandler.EnqueueActionRequest(new IndexEquipmentRequest(1));
            }
        }

        public void OnSelectPrevItem(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Debug.Log($"Enqueueing index previous request");
                _requestHandler.EnqueueActionRequest(new IndexEquipmentRequest(-1));
            }
        }

        public void OnQuickThrowEquip(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnEquipItem1(InputAction.CallbackContext context)
        {
            if (context.started)
                _requestHandler.EnqueueActionRequest(new SwitchEquipmentRequest(1));
        }

        public void OnEquipItem2(InputAction.CallbackContext context)
        {
            if (context.started)
                _requestHandler.EnqueueActionRequest(new SwitchEquipmentRequest(2));
        }

        public void OnEquipItem3(InputAction.CallbackContext context)
        {
            if (context.started)
                _requestHandler.EnqueueActionRequest(new SwitchEquipmentRequest(3));
        }

        public void OnEquipItem4(InputAction.CallbackContext context)
        {
            if (context.started)
                _requestHandler.EnqueueActionRequest(new SwitchEquipmentRequest(4));
        }

        public void OnEquipItem5(InputAction.CallbackContext context)
        {
            if (context.started)
                _requestHandler.EnqueueActionRequest(new SwitchEquipmentRequest(5));
        }

        public void OnEquipItem6(InputAction.CallbackContext context)
        {
            if (context.started)
                _requestHandler.EnqueueActionRequest(new SwitchEquipmentRequest(6));
        }

        public void OnEquipItem7(InputAction.CallbackContext context)
        {
            if (context.started)
                _requestHandler.EnqueueActionRequest(new SwitchEquipmentRequest(7));
        }

        public void OnToggleFlashlight(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnSprint(InputAction.CallbackContext context) { }

        public void OnDebugRespawn(InputAction.CallbackContext context) { }

        public void OnHideDebugInfo(InputAction.CallbackContext context) { }

    }
}