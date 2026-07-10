
using System;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;

namespace Movement.Core.Movement.DataStructures
{
    public struct BiteResult : IActionResult
    {
        public Type ResultType => typeof(BiteResult);

        public bool Approved => _approved;
        private readonly bool _approved;

        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;

        public BiteResult(bool approved)
        {
            _approved = approved;
            _phase = ActionPhase.Override;
        }
    }
}