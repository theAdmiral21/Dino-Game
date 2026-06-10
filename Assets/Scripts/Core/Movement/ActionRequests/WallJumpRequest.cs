
using System;

namespace Movement.Core.Movement.DataStructures
{
    public struct WallJumpRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(WallJumpRequest);
        public readonly bool Requested;
        public WallJumpRequest(bool requested)
        {
            Requested = requested;
        }
    }
}