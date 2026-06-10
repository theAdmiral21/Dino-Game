using System;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Enums;

namespace Movement.Core.Movement.DataStructures
{
    public struct QuickStepResult : IActionResult
    {
        public bool Approved => _approved;
        private readonly bool _approved;
        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;
        public readonly float Direction;
        public Type ResultType => typeof(QuickStepResult);
        public QuickStepResult(bool approved, float direction, ActionPhase phase)
        {
            _approved = approved;
            Direction = direction;
            _phase = phase;
        }
    }
}