
using System;

namespace Movement.Core.Movement.DataStructures
{
    public struct ShootRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(ShootRequest);

        public ShootRequest(bool approved = true)
        {
        }
    }
}