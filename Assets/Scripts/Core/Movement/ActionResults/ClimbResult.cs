
using System;
using UnityEngine;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;
using Primitives.Physics.Enums;

namespace Movement.Core.Movement.DataStructures
{
    public struct ClimbResult : IActionResult
    {
        public Type ResultType => typeof(ClimbResult);

        public bool Approved => _approved;
        private readonly bool _approved;

        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;

        public readonly Vector2 InputDir;
        public readonly ClimbType Climb;

        public ClimbResult(bool approved, Vector2 inputDir, ClimbType climb)
        {
            _approved = approved;
            _phase = ActionPhase.Continuous;
            InputDir = inputDir;
            Climb = climb;
        }
    }
}