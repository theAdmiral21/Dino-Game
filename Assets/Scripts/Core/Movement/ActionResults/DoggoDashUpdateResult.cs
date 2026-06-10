
using System;
using Movement.Core.Abstractions;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;
using Primitives.Input;

namespace Movement.Core.Movement.DataStructures
{
    public struct DoggoDashUpdateResult : IActionResult
    {
        public Type ResultType => typeof(DoggoDashUpdateResult);

        public bool Approved => _approved;
        private readonly bool _approved;

        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;

        public readonly IDoggoDashState DashState;

        public DoggoDashUpdateResult(bool approved, IDoggoDashState dashState)
        {
            _approved = approved;
            _phase = ActionPhase.Continuous;
            DashState = dashState;
        }
    }
}