using System;
using UnityEngine;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Enums;

namespace Movement.Core.Movement.DataStructures
{
    public struct TeleportResult : IActionResult
    {
        public bool Approved => _approved;
        private readonly bool _approved;
        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;
        public readonly Vector2 CurrentPosition;
        public readonly Vector2 Destination;

        public Type ResultType => typeof(TeleportResult);
        public TeleportResult(bool approved, Vector2 currentPosition, Vector2 destination)
        {
            _approved = approved;
            CurrentPosition = currentPosition;
            Destination = destination;
            _phase = ActionPhase.Override;
        }
    }
}