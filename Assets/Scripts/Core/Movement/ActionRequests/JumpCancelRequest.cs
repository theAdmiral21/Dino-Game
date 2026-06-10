using System;

namespace Movement.Core.Movement.DataStructures
{
    /// <summary>
    /// Public struct sent from application layer to cancel a jump from Core.
    /// </summary>
    public struct JumpCancelRequest : IActionRequest
    {
        public readonly bool Requested;
        public Type RequestType => typeof(JumpCancelRequest);

        public JumpCancelRequest(bool requested) => Requested = requested;
    }
}