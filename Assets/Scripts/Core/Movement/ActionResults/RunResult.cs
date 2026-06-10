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
        public bool IsZooming;
        public Type ResultType => typeof(RunResult);
        public RunResult(bool approved, Vector2 value, RunType type, bool isZooming, ActionPhase phase)
        {
            _approved = approved;
            Value = value;
            Type = type;
            IsZooming = isZooming;
            _phase = phase;
        }
    }
}