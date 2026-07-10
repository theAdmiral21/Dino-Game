using System;

namespace Primitives.Stats.DataStructures
{
    [Serializable]
    public struct BiteStats
    {
        public Type RuntimeType => typeof(BiteStats);
        public Stat Damage;
        public Stat CoolDown;
    }
}
