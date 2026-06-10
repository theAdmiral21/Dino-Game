using System;

namespace Movement.Core.Movement.DataStructures
{
    /// <summary>
    /// Public struct sent from application layer to request a jump from Core.
    /// </summary>
    public readonly struct WallSlideRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(WallSlideRequest);
        public readonly bool Requested;
        public WallSlideRequest(bool requested)
        {
            Requested = requested;
        }

    }
}