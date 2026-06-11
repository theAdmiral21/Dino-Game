
using System;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;
using Primitives.Input;

namespace Movement.Core.Movement.DataStructures
{
    public struct DodgeResult : IActionResult
    {
        public Type ResultType => typeof(DodgeResult);

        public bool Approved => _approved;
        private readonly bool _approved;

        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;

        public InputDirection DodgeDirection => _dodgeDirection;
        private readonly InputDirection _dodgeDirection;

        public DodgeResult(bool approved, InputDirection dodgeDirection)
        {
            _approved = approved;
            _phase = ActionPhase.Impulse;
            _dodgeDirection = dodgeDirection;
        }
    }
}