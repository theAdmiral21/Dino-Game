using System;

namespace Primitives.Stats.DataStructures
{
    [Serializable]
    public struct DashStats
    {
        public Type RuntimeType => typeof(DashStats);

        public Stat TotalDashes;
        public Stat DashDist;
        public Stat DashTime;
        public Stat DashBufferTime;
    }
}