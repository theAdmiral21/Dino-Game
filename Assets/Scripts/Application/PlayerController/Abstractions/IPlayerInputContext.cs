using System;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerController.Application.Inputs.DataStructures;

namespace PlayerController.Application.Abstractions
{
    public interface IPlayerInputContext
    {
        public InputInfo InputInfo { get; }
        public Vector2 MoveVector { get; }
        public float VectorX { get; }
        public float VectorY { get; }
        public bool JumpPressed { get; }
        public bool SlidePressed { get; }
        public bool JumpBuffered { get; }
        public bool WallJumpBuffered { get; }
        public bool PerformHowl { get; }

        // Events
        public event Action<Vector2> OnXInputChanged;
        public event Action<Vector2> OnXInputStopped;
        public event Action<bool> OnJumpStarted;
        public event Action<bool> OnJumpStopped;
        public event Action<bool> OnSlideStarted;
        public event Action<bool> OnSlideStopped;
        public event Action<bool> OnGrabStarted;
        public event Action OnGrabStopped;
        public event Action OnBark;
        public event Action OnHowl;
        public event Action<bool> OnSprintStarted;
        public event Action<bool> OnSprintStopped;


        public PlayerInput InputObject { get; }

        public void ChangePlayerActionMap(string actionMap);
        public bool CanCoyoteJump();
    }
}