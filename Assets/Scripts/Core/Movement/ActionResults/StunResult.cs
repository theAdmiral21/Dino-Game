using System;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;

namespace Movement.Core.Movement.DataStructures
{
    public struct StunResult : IImmediateResult
    {
        public bool Approved => _approved;
        private readonly bool _approved;
        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;
        public Type ResultType => typeof(StunResult);
        public StunResult(bool approved)
        {
            _approved = approved;
            _phase = ActionPhase.Impulse;
        }
    }
}