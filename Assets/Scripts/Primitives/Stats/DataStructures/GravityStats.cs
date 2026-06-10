using System;

namespace Primitives.Stats.DataStructures
{
    [Serializable]
    public struct GravityStats
    {
        public Type RuntimeType => typeof(GravityStats);
        public Stat BaseGravity;
        public Stat SlowFall;
        public Stat FastFall;
    }
}