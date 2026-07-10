using System;

namespace Primitives.Stats.DataStructures
{
    [Serializable]
    public struct HealthStats
    {
        public Type RuntimeType => typeof(HealthStats);
        public Stat TotalHealth;
    }
}
