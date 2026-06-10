
using System;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;
using Primitives.Input;

namespace Movement.Core.Movement.DataStructures
{
    public struct DoggoDashResult : IActionResult
    {
        public Type ResultType => typeof(DoggoDashResult);

        public bool Approved => _approved;
        private readonly bool _approved;

        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;

        public InputDirection Direction => _direction;
        private readonly InputDirection _direction;

        public DoggoDashResult(bool approved, InputDirection direction)
        {
            _approved = approved;
            _direction = direction;
            _phase = ActionPhase.Impulse;
        }
    }
}