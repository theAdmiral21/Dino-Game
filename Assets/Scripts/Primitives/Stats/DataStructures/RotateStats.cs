using System;

namespace Primitives.Stats.DataStructures
{
    [Serializable]
    public struct RotateStats
    {
        public Type RuntimeType => typeof(RotateStats);

        public Stat AngularVelocity;
        public Stat AngularAccel;
    }
}