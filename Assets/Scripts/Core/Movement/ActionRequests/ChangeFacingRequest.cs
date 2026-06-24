
using System;

namespace Movement.Core.Movement.DataStructures
{
    public struct ChangeFacingRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(ChangeFacingRequest);
        public readonly bool FaceLeft;
        public ChangeFacingRequest(bool faceLeft)
        {
            FaceLeft = faceLeft;
        }
    }
}