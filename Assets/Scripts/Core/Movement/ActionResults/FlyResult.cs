using System;
using UnityEngine;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Enums;

namespace Movement.Core.Movement.DataStructures
{
    public struct FlyResult : IActionResult
    {
        public bool Approved => _approved;
        private readonly bool _approved;
        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;
        public readonly Vector2 Input;

        public Type ResultType => typeof(FlyResult);
        public FlyResult(bool approved, Vector2 input)
        {
            _approved = approved;
            Input = input;
            _phase = ActionPhase.Continuous;
        }
    }
}