using System;
using Movement.Core.Movement.DataStructures;

namespace PlayerController.Core.Movement.DataStructures
{
    public struct InteractRequest : IActionRequest
    {
        public bool Requested => _requested;
        private bool _requested;

        public Type RequestType => typeof(InteractRequest);

        public InteractRequest(bool requested)
        {
            _requested = requested;
        }
    }
}