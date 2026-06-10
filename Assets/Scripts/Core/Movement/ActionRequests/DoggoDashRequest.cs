
using System;
using UnityEngine;

namespace Movement.Core.Movement.DataStructures
{
    public struct DoggoDashRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(DoggoDashRequest);
        public readonly bool Approved;
        public Vector2 Direction => _direction;
        private Vector2 _direction;
        public DoggoDashRequest(Vector2 direction)
        {
            Approved = true;
            _direction = direction;
        }
    }
}