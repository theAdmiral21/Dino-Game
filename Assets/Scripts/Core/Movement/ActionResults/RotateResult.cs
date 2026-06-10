
using System;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;

namespace Movement.Core.Movement.DataStructures
{
    public struct RotateResult : IActionResult
    {
        public Type ResultType => typeof(RotateResult);

        public bool Approved => _approved;
        private readonly bool _approved;

        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;

        public float Omega => _omega;
        private readonly float _omega;

        public RotateResult(bool approved, float omega, ActionPhase phase)
        {
            _approved = approved;
            _phase = phase;
            _omega = omega;
        }
    }
}