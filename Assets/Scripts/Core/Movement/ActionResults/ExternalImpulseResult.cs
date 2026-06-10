using System;
using UnityEngine;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Enums;

namespace Movement.Core.Movement.DataStructures
{
    /// <summary>
    /// This is where Core tells the requesting application level object the result of the jump.
    /// </summary>
    public struct ExternalImpulseResult : IActionResult
    {
        public bool Approved => _approved;
        private readonly bool _approved;
        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;
        public Vector2 Velocity => _velocity;
        private readonly Vector2 _velocity;
        public float Gravity => _gravity;
        private readonly float _gravity;

        public Type ResultType => typeof(ExternalImpulseResult);

        public ExternalImpulseResult(bool approved, Vector2 velocity, float gravity)
        {
            _approved = approved;
            _velocity = velocity;
            _gravity = gravity;
            _phase = ActionPhase.Impulse;
        }
    }
}