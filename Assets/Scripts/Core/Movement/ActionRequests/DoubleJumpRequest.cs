
using System;

namespace Movement.Core.Movement.DataStructures
{
    public struct DoubleJumpRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(DoubleJumpRequest);
        public readonly bool Requested;
        public DoubleJumpRequest(bool requested)
        {
            Requested = requested;
        }
    }
}