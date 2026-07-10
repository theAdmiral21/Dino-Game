
using System;

namespace Movement.Core.Movement.DataStructures
{
    public struct BiteRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(BiteRequest);

        public BiteRequest(bool requested = true)
        {
        }
    }
}