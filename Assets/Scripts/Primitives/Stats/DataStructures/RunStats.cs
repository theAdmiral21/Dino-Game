using System;

namespace Primitives.Stats.DataStructures
{
    [Serializable]
    public struct RunStats
    {
        public Type RuntimeType => typeof(RunStats);
        public Stat RunSpeed;
        public Stat RunAccel;
        public Stat BrakeAccel;
        public Stat CrouchWalkSpeed;
        public Stat CrouchWalkAccel;
        public Stat CrouchWalkBrake;
    }
}