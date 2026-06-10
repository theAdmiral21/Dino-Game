using System;

namespace Primitives.Stats.DataStructures
{
    [Serializable]
    public struct SprintStats
    {
        public Type RuntimeType => typeof(SprintStats);

        public Stat SprintSpeed;
        public Stat SprintAccel;
    }
}