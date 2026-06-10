using System;

namespace Primitives.Stats.DataStructures
{
    [Serializable]
    public struct LongJumpStats
    {
        public Type RuntimeType => typeof(LongJumpStats);

        public Stat LongJumpFarWindow;
        public Stat LongJumpFarHeight;
        public Stat LongJumpFarApexTime;
        public Stat LongJumpFarSpeed;
        public Stat LongJumpMedHeight;
        public Stat LongJumpMedApexTime;
        public Stat LongJumpMedSpeed;
    }
}