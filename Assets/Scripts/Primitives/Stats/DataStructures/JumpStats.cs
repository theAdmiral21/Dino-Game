using System;

namespace Primitives.Stats.DataStructures
{
    [Serializable]
    public struct JumpStats
    {
        public Type RuntimeType => typeof(JumpStats);

        public Stat TotalJumps;
        public Stat JumpHeight;
        public Stat JumpApexTime;
        public Stat JumpBufferTime;
        public Stat CoyoteTime;
    }
}