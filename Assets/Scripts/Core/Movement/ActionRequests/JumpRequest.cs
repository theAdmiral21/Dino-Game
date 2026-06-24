using System;
using Movement.Core.DataStructures;
using Primitives.Physics;

namespace Movement.Core.Movement.DataStructures
{
    /// <summary>
    /// Public struct sent from application layer to request a jump from Core.
    /// </summary>
    public struct JumpRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(JumpRequest);
        public readonly bool Requested;
        public JumpType JumpType;
        public JumpContext Context { get; set; }
        public JumpRequest(bool requested, JumpType jumpType)
        {
            Requested = requested;
            JumpType = jumpType;
            Context = new();
        }

    }
}