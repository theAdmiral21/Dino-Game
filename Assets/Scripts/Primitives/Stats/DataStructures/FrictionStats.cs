using System;

namespace Primitives.Stats.DataStructures
{
    [Serializable]
    public struct FrictionStats
    {
        public Type RuntimeType => typeof(FrictionStats);
        public Stat Friction;
    }
}