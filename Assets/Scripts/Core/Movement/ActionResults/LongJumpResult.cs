
using System;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;

namespace Movement.Core.Movement.DataStructures 
{
    public struct LongJumpResult : IActionResult
    {
        public Type ResultType => typeof(LongJumpResult);

        public bool Approved => _approved;
        private readonly bool _approved;

        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;

        public LongJumpResult(bool approved,ActionPhase phase)
        {
            _approved = approved;
            _phase = phase;
        }
    }
}