using System;

namespace Movement.Core.Movement.DataStructures
{
    /// <summary>
    /// Public struct sent from application layer to request a jump from Core.
    /// </summary>
    public readonly struct StunRequest : IActionRequest
    {
        public Type RequestType => typeof(StunRequest);
        public readonly bool Requested;
        public readonly float Duration;
        public StunRequest(bool request, float duration)
        {
            Requested = request;
            Duration = duration;
        }
    }
}