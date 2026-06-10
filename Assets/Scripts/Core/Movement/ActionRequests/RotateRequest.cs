
using System;

namespace Movement.Core.Movement.DataStructures
{
    public struct RotateRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(RotateRequest);
        public float Omega => _omega;
        private readonly float _omega;
        public RotateRequest(float omega)
        {
            _omega = omega;
        }
    }
}