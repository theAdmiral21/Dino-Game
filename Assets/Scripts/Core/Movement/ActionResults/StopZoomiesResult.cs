
using System;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;

namespace Movement.Core.Movement.DataStructures 
{
    public struct StopZoomiesResult : IActionResult
    {
        public Type ResultType => typeof(StopZoomiesResult);

        public bool Approved => _approved;
        private readonly bool _approved;

        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;

        public StopZoomiesResult(bool approved,ActionPhase phase)
        {
            _approved = approved;
            _phase = phase;
        }
    }
}