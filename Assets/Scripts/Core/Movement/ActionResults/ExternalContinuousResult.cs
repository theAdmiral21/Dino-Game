using System;
using UnityEngine;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Enums;

namespace Movement.Core.Movement.DataStructures
{
    /// <summary>
    /// This is where Core tells the requesting application level object the result of the jump.
    /// </summary>
    public struct ExternalContinuousResult : IActionResult
    {
        public bool Approved => _approved;
        private readonly bool _approved;
        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;
        public Vector2 Velocity => _velocity;
        private readonly Vector2 _velocity;

        public Type ResultType => typeof(ExternalContinuousResult);

        public ExternalContinuousResult(bool approved, Vector2 velocity)
        {
            _approved = approved;
            _velocity = velocity;
            _phase = ActionPhase.Continuous;
        }
    }
}