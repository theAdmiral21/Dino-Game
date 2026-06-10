using System;

namespace Primitives.Stats.DataStructures
{
    [Serializable]
    public struct InvincibilityStats
    {
        public Type RuntimeType => typeof(InvincibilityStats);
        public Stat InvincibilityDuration;
    }
}