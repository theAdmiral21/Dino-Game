using System;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;
using Primitives.Physics;

namespace Movement.Core.Movement.DataStructures
{
    public struct RunStopResult : IActionResult
    {
        public bool Approved => _approved;
        private readonly bool _approved;
        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;
        public RunType RunType;
        public Type ResultType => typeof(RunResult);

        public RunStopResult(bool approved, RunType runType, ActionPhase phase)
        {
            _approved = approved;
            RunType = runType;
            _phase = phase;
        }
    }
}