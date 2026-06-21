using System;

namespace Movement.Core.Movement.DataStructures
{
    public struct FrictionRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(FrictionRequest);
        public readonly bool Requested;
        public FrictionRequest(bool request = true)
        {
            Requested = request;
        }
    }
}