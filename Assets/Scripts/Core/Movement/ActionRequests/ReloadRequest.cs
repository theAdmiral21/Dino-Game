
using System;

namespace Movement.Core.Movement.DataStructures
{
    public struct ReloadRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(ReloadRequest);

        public ReloadRequest(bool requested = true)
        {
        }
    }
}