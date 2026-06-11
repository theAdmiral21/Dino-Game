using System;
using Movement.Core.Abstractions;
using Movement.Core.Inputs;
using Movement.Core.Movement.DataStructures;
using PlayerController.Core.Movement.DataStructures;
using Primitives.Input;
using Unity.Common.Unity;
using UnityEngine;
using UnityEngine.InputSystem;
using Primitives.Physics;

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
        public bool GrabPressed => _grabPressed;
        private bool _grabPressed =>
                                    (_actions.InGame.Grab.phase == InputActionPhase.Started) ||
                                    (_actions.InGame.Grab.phase == InputActionPhase.Performed);
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
        public event Action<IActionRequest> JumpCancel;
        public event Action<IActionRequest> Run;
        public event Action<IActionRequest> RunStop;
        public event Action<IActionRequest> QuickStep;
        public event Action<IActionRequest> Teleport;
        public event Action<IActionRequest> Bark;
        public event Action<IActionRequest> Pause;
        public event Action<IActionRequest> Scent;

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
                float tapDir = Mathf.Sign(context.ReadValue<Vector2>().x);
                if (Mathf.Abs(tapDir) < 0.5f) return;
                if (tapDir == _prevTap && (Time.time - _lastTapTime < DOUBLE_TAP_WINDOW))
                {
                    // Debug.Log($"Got quick step! {Time.frameCount}");
                    // Debug.Log($" tapDir: {tapDir}; Prev tap: {_prevTap}; time: {Time.time}; _lastTapTime: {_lastTapTime}");

                    // QuickStep?.Invoke(new QuickStepRequest(true, tapDir));
                    _requestHandler.EnqueueActionRequest(new QuickStepRequest(true, tapDir));
                }
                // Get the tap time
                _lastTapTime = Time.time;
                _prevTap = tapDir;
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
            Debug.Log($"Received run stop input");
            // RunStop?.Invoke(new RunStopRequest(true, context.ReadValue<Vector2>()));
            _requestHandler.EnqueueActionRequest(new RunStopRequest(true, context.ReadValue<Vector2>()));
            // _moveInput.x = 0;
            _moveInput = context.ReadValue<Vector2>();
        }

        private void Update()
        {
            if (MoveInput != Vector2.zero)
            {
                // Run?.Invoke(new RunRequest(true, MoveInput));
                _requestHandler.EnqueueActionRequest(new RunRequest(true, MoveInput));
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
        public void OnScent(InputAction.CallbackContext context)
        {
            // if (context.started)
            // {
            //     Debug.Log("Scent requested");
            //     // Scent?.Invoke(new ScentRequest());
            //     _requestHandler.EnqueueActionRequest(new ScentRequest());

            // }
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

        public void OnTeleport(InputAction.CallbackContext context)
        {
            // if (context.started)
            // {
            //     Vector2 dest = _moveInput;

            //     Vector2 currentPos = GetPosition();

            //     // Teleport?.Invoke(new TeleportRequest(true, currentPos, dest));
            //     _requestHandler.EnqueueActionRequest(new TeleportRequest(true, currentPos, dest));
            // }
        }

        public void OnActivateZoomies(InputAction.CallbackContext context)
        {
            // if (context.started)
            // {
            //     _requestHandler.EnqueueActionRequest(new ZoomiesRequest());
            // }
        }

        private Vector2 GetPosition()
        {
            if (transform.parent == null)
            {
                return transform.position;
            }
            else
            {
                return transform.parent.position;
            }
        }

        public void OnDoggoDash(InputAction.CallbackContext context)
        {
            // if (context.started)
            // {
            //     Debug.Log("Dash requested");
            //     _requestHandler.EnqueueActionRequest(new DoggoDashRequest(_leftStick));
            // }
        }

        public void OnChangeMoveMode(InputAction.CallbackContext context)
        {
            // if (context.started)
            // {
            //     _requestHandler.EnqueueActionRequest(new SwitchMovementRequest(true));
            // }
        }

        public void OnSprint(InputAction.CallbackContext context) { }

        public void OnLookUp(InputAction.CallbackContext context) { }

        public void OnHowl(InputAction.CallbackContext context) { }


        public void OnGrab(InputAction.CallbackContext context) { }

        public void OnDrop(InputAction.CallbackContext context) { }

        public void OnDebugRespawn(InputAction.CallbackContext context) { }

        public void OnYInput(InputAction.CallbackContext context) { }

        public void OnDown(InputAction.CallbackContext context) { }

        public void OnHideDebugInfo(InputAction.CallbackContext context) { }

        public void OnRightTriggerPull(InputAction.CallbackContext context) { }

        public void OnLeftTriggerPull(InputAction.CallbackContext context) { }

        public void OnWallJump(InputAction.CallbackContext context) { }

        public void OnSlide(InputAction.CallbackContext context) { }
    }
}