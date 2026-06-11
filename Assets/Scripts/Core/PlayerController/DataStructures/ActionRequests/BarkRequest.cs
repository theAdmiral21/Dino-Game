using System;
using Movement.Core.Movement.DataStructures;

namespace PlayerController.Core.Movement.DataStructures
{
    public struct BarkRequest : IActionRequest
    {
        public bool Requested => _requested;
        private bool _requested;

        public Type RequestType => typeof(BarkRequest);

        public BarkRequest(bool requested)
        {
            _requested = requested;
        }
    }
}