
using System;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;

namespace Movement.Core.Movement.DataStructures
{
    public struct FrictionResult : IActionResult
    {
        public Type ResultType => typeof(FrictionResult);

        public bool Approved => _approved;
        private readonly bool _approved;

        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;

        public FrictionResult(bool approved, ActionPhase phase = ActionPhase.Continuous)
        {
            _approved = approved;
            _phase = phase;
        }
    }
}