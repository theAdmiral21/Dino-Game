using System;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;
using Primitives.Physics.Enums;

namespace Movement.Core.Movement.DataStructures
{
    public struct ClimbStopResult : IActionResult
    {
        public bool Approved => _approved;
        private readonly bool _approved;
        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;
        public Type ResultType => typeof(ClimbStopResult);

        public readonly ClimbObject ClimbingSurface;

        public ClimbStopResult(bool approved, ClimbObject climbingSurface)
        {
            _approved = approved;
            _phase = ActionPhase.Continuous;
            ClimbingSurface = climbingSurface;
        }
    }
}