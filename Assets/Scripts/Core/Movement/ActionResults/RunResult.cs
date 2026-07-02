using System;
using UnityEngine;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Enums;
using Primitives.Physics;

namespace Movement.Core.Movement.DataStructures
{
    public struct RunResult : IActionResult
    {
        public bool Approved => _approved;
        private readonly bool _approved;
        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;
        public RunType Type;
        public readonly Vector2 Value;
        public Type ResultType => typeof(RunResult);
        public readonly bool IsBackingUp;
        public RunResult(bool approved, bool isBackingUp, Vector2 value, RunType type, ActionPhase phase)
        {
            _approved = approved;
            IsBackingUp = isBackingUp;
            Value = value;
            Type = type;
            _phase = phase;
        }
    }
}