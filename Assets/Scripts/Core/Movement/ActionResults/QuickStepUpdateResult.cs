using System;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Enums;
using Movement.Core.Abstractions;

namespace Movement.Core.Movement.DataStructures
{
    public struct QuickStepUpdateResult : IActionResult
    {
        public bool Approved => _approved;
        private readonly bool _approved;
        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;
        public readonly IQuickStepState QuickStepState;
        public Type ResultType => typeof(QuickStepUpdateResult);
        public QuickStepUpdateResult(bool approved, IQuickStepState quickStepState, ActionPhase phase)
        {
            _approved = approved;
            QuickStepState = quickStepState;
            _phase = phase;
        }
    }
}