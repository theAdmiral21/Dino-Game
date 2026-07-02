
using System;
using UnityEngine;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;

namespace Movement.Core.Movement.DataStructures
{
    public struct LungeResult : IActionResult
    {
        public Type ResultType => typeof(LungeResult);

        public bool Approved => _approved;
        private readonly bool _approved;

        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;
        public readonly Vector2 Direction;

        public LungeResult(bool approved, Vector2 direction)
        {
            _approved = approved;
            _phase = ActionPhase.Impulse;
            Direction = direction;
        }
    }
}