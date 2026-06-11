using System;

namespace Movement.Core.Movement.DataStructures
{
    /// <summary>
    /// Public struct sent from application layer to cancel a jump from Core.
    /// </summary>
    public struct ScentRequest : IActionRequest
    {
        public bool Requested;

        public Type RequestType => typeof(ScentRequest);
    }
}