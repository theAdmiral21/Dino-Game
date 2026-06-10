using System;
using UnityEngine;

namespace Movement.Core.Movement.DataStructures
{
    /// <summary>
    /// Public struct sent from application layer to request a jump from Core.
    /// </summary>
    public readonly struct KnockBackRequest : IActionRequest
    {
        public Type RequestType => typeof(KnockBackRequest);
        public readonly bool Requested;
        public readonly Vector2 Velocity;
        public readonly float ApexTime;
        public KnockBackRequest(bool request, float apexTime, Vector2 velocity)
        {
            Requested = request;
            ApexTime = apexTime;
            Velocity = velocity;
        }
    }
}