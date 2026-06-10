using System;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Enums;
using UnityEngine;

namespace Movement.Core.Movement.DataStructures
{
    public struct QuickStepStopResult : IActionResult
    {
        public bool Approved => _approved;
        private readonly bool _approved;
        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;
        public readonly Vector2 MoveInput;
        public readonly bool SprintPressed;
        public Type ResultType => typeof(QuickStepStopResult);
        public QuickStepStopResult(bool approved, Vector2 moveInput, bool sprintPressed, ActionPhase phase)
        {
            _approved = approved;
            MoveInput = moveInput;
            SprintPressed = sprintPressed;
            _phase = phase;
        }
    }
}