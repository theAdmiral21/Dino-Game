using System;

namespace Primitives.Stats.DataStructures
{
    [Serializable]
    public struct LungeStats
    {
        public Type RuntimeType => typeof(LungeStats);

        public Stat TotalLunges;
        public Stat LungeDistance;
        public Stat LungeHeight;
        public Stat LungeApexTime;
        public Stat LungeDuration;
        public Stat LungeDamage;
        public Stat LungeCoolDown;
    }
}