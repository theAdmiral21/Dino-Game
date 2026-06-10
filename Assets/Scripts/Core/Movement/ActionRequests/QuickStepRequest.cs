using System;

namespace Movement.Core.Movement.DataStructures
{
    /// <summary>
    /// Public struct sent from application layer to request a jump from Core.
    /// </summary>
    public readonly struct QuickStepRequest : IActionRequest
    {
        public Type RequestType => typeof(QuickStepRequest);
        public readonly bool Requested;
        public readonly float Direction;
        public QuickStepRequest(bool request, float direction)
        {
            Requested = request;
            Direction = direction;
        }
    }
}