using System;

namespace Primitives.Stats.DataStructures
{
    [Serializable]
    public struct DodgeStats
    {
        public Type RuntimeType => typeof(DodgeStats);

        public Stat TotalDodges;
        public Stat DodgeDist;
        public Stat DodgeTime;
        public Stat DodgeBufferTime;
    }
}