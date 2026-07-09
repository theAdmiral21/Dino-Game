
using System;

namespace Movement.Core.Movement.DataStructures
{
    public struct CrouchRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(CrouchRequest);
        public readonly bool Requested;

        public readonly bool CanStand;

        public CrouchRequest(bool canStand)
        {
            Requested = true;
            CanStand = canStand;
        }
    }
}