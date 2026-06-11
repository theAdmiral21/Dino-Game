using System;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;

namespace PlayerController.Core.Movement.DataStructures
{
    public struct ScentResult : IActionResult
    {
        public Type ResultType => typeof(ScentResult);
        public bool Approved => _approved;
        private readonly bool _approved;
        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;

        public ScentResult(bool approved, ActionPhase phase)
        {
            _approved = approved;
            _phase = phase;
        }
    }
}