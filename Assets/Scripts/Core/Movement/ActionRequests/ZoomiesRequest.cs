
using System;

namespace Movement.Core.Movement.DataStructures
{
    public struct ZoomiesRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(ZoomiesRequest);
        public readonly bool Requested;
        public ZoomiesRequest(bool requested = true)
        {
            Requested = requested;
        }
    }
}