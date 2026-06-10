
using System;
using UnityEngine;

namespace Movement.Core.Movement.DataStructures
{
    public struct ExternalImpulseRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(ExternalImpulseRequest);
        public bool Approved => _approved;
        private readonly bool _approved;
        public Vector2 Velocity => _velocity;
        private readonly Vector2 _velocity;
        public float Gravity => _gravity;
        private readonly float _gravity;
        public ExternalImpulseRequest(Vector2 velocity, float gravity)
        {
            _approved = true;
            _velocity = velocity;
            _gravity = gravity;
        }
    }
}