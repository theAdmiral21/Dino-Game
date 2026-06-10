
using System;
using UnityEngine;

namespace Movement.Core.Movement.DataStructures
{
    public struct ExternalContinuousRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(ExternalContinuousRequest);
        public bool Approved => _approved;
        private readonly bool _approved;

        public Vector2 Velocity => _velocity;
        private readonly Vector2 _velocity;
        public ExternalContinuousRequest(Vector2 velocity)
        {
            _velocity = velocity;
            _approved = true;
        }
    }
}