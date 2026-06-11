
using System;
using UnityEngine;

namespace Movement.Core.Movement.DataStructures
{
    public struct DodgeRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(DodgeRequest);
        public readonly bool Approved;
        public Vector2 Direction => _direction;
        private Vector2 _direction;
        public DodgeRequest(Vector2 direction)
        {
            Approved = true;
            _direction = direction;
        }
    }
}