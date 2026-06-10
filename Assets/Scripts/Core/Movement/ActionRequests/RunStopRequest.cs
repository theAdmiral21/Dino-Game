using System;
using UnityEngine;

namespace Movement.Core.Movement.DataStructures
{
    /// <summary>
    /// Public struct sent from application layer to request a jump from Core.
    /// </summary>
    public readonly struct RunStopRequest : IActionRequest
    {
        public Type RequestType => typeof(RunStopRequest);
        public readonly bool Requested;
        public readonly Vector2 Value;
        public RunStopRequest(bool request, Vector2 value)
        {
            Requested = request;
            Value = value;
        }
    }
}