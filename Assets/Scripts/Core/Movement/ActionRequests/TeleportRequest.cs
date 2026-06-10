using System;
using UnityEngine;

namespace Movement.Core.Movement.DataStructures
{
    /// <summary>
    /// Public struct sent from application layer to request a jump from Core.
    /// </summary>
    public readonly struct TeleportRequest : IActionRequest
    {
        public Type RequestType => typeof(TeleportRequest);
        public readonly bool Requested;
        public readonly Vector2 Destination;
        public readonly Vector2 CurrentPosition;
        public TeleportRequest(bool request, Vector2 currentPosition, Vector2 destination)
        {
            Requested = request;
            CurrentPosition = currentPosition;
            Destination = destination;
        }
    }
}