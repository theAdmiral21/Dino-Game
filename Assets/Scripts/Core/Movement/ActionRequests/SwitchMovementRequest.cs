
using System;

namespace Movement.Core.Movement.DataStructures
{
    public struct SwitchMovementRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(SwitchMovementRequest);
        public readonly bool Approved;
        public SwitchMovementRequest(bool approved)
        {
            Approved = approved;
        }
    }
}