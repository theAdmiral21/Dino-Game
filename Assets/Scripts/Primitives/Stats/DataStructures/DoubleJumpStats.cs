using System;

namespace Primitives.Stats.DataStructures
{
    [Serializable]
    public struct DoubleJumpStats
    {
        public Type RuntimeType => typeof(DoubleJumpStats);

        public Stat DoubleJumpHeight;
        public Stat DoubleJumpApexTime;
    }
}