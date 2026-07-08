using System;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;


namespace PlayerController.Core.Movement.Abstractions
{
    public struct InteractResult : IActionResult
    {
        public bool Approved => _approved;
        private bool _approved;

        public Type ResultType => typeof(InteractResult);

        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;

        public InteractResult(bool approved, ActionPhase phase)
        {
            _approved = approved;
            _phase = phase;
        }
    }
}