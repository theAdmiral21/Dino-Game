using System;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;
using UnityEngine;

namespace Movement.Core.Movement.DataStructures
{
    public struct KnockBackResult : IImmediateResult
    {
        public bool Approved => _approved;
        private readonly bool _approved;
        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;
        public Vector2 Velocity { get; private set; }
        public Type ResultType => typeof(KnockBackResult);
        public float ApexTime { get; private set; }
        public KnockBackResult(bool approved, float apexTime, Vector2 velocity)
        {
            _approved = approved;
            ApexTime = apexTime;
            Velocity = velocity;
            _phase = ActionPhase.Impulse;
        }
    }
}