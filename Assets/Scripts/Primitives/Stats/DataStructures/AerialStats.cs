using System;

namespace Primitives.Stats.DataStructures
{
    [Serializable]
    public struct AerialStats
    {
        public Type RuntimeType => typeof(AerialStats);

        public Stat AerialAccel;
        public Stat AerialBrake;
    }
}