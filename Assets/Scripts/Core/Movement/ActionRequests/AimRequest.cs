
using System;
using UnityEngine;

namespace Movement.Core.Movement.DataStructures
{
    public struct AimRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(AimRequest);
        public readonly Vector2 MousePosition;
        public AimRequest(Vector2 mousePosition)
        {
            MousePosition = mousePosition;
        }
    }
}