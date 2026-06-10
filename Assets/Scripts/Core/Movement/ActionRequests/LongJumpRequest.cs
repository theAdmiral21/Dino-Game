
using System;

namespace Movement.Core.Movement.DataStructures
{
    public struct LongJumpRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(LongJumpRequest);
        public readonly bool Approved;
        public LongJumpRequest(bool approved) => Approved = approved;
    }
}