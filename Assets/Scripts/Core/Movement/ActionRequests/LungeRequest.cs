
using System;
using UnityEngine;

namespace Movement.Core.Movement.DataStructures
{
    public struct LungeRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(LungeRequest);
        public readonly Vector2 Direction;
        public LungeRequest(Vector2 direction)
        {
            Direction = direction;
        }
    }
}