using System;

namespace Primitives.Stats.DataStructures
{
    [Serializable]
    public struct WallStats
    {
        public Type RuntimeType => typeof(WallStats);

        public Stat WallSlideSpeed;
        public Stat WallJumpHeight;
        public Stat WallJumpApexTime;
        public Stat WallJumpXDuration;
        public Stat WallJumpXDistance;
        public Stat WallJumpBufferTime;
    }
}