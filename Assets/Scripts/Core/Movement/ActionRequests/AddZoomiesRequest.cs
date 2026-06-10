
using System;

namespace Movement.Core.Movement.DataStructures
{
    public struct AddZoomiesRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(AddZoomiesRequest);
        public readonly bool Requested;
        public readonly float Amount;

        public AddZoomiesRequest(float amount, bool requested = true)
        {
            Amount = amount;
            Requested = requested;
        }
    }
}