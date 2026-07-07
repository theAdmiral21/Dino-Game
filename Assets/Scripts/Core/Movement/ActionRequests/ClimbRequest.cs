
using System;
using Primitives.Physics.Enums;
using UnityEngine;

namespace Movement.Core.Movement.DataStructures
{
    public struct ClimbRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(ClimbRequest);
        public readonly Vector2 InputDir;
        public ClimbType Climb;
        public ClimbRequest(Vector2 inputDir, ClimbType climb = ClimbType.None)
        {
            InputDir = inputDir;
            Climb = climb;
        }
    }
}