
using System;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;

namespace Movement.Core.Movement.DataStructures
{
    public struct CrouchResult : IActionResult
    {
        public Type ResultType => typeof(CrouchResult);

        public bool Approved => _approved;
        private readonly bool _approved;

        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;

        public readonly bool CrouchValue;

        public CrouchResult(bool approved, bool crouchValue)
        {
            _approved = approved;
            _phase = ActionPhase.Impulse; // I think this should be impulse because it happens immediately. You change state. But this is weird because it doesn't need to go to the calculator.
            CrouchValue = crouchValue;
        }
    }
}