using System;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;
using Primitives.Physics;

namespace Movement.Core.Movement.DataStructures
{
    /// <summary>
    /// This is where Core tells the requesting application level object the result of the jump.
    /// </summary>
    public struct JumpResult : IActionResult
    {
        public bool Approved => _approved;
        private readonly bool _approved;
        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;
        public readonly JumpType Type;
        public readonly int JumpsConsumed;
        public readonly float Dir;

        public Type ResultType => typeof(JumpResult);

        public JumpResult(bool approved, JumpType type, int jumpsConsumed, float dir, ActionPhase phase)
        {
            _approved = approved;
            Type = type;
            JumpsConsumed = jumpsConsumed;
            Dir = dir;
            _phase = phase;
        }
    }
}