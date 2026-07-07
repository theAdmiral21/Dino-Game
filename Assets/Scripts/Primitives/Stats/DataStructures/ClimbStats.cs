using System;

namespace Primitives.Stats.DataStructures
{
    [Serializable]
    public struct ClimbStats
    {
        public Type RuntimeType => typeof(ClimbStats);

        public Stat StairSpeed;
        public Stat StairAccel;
        public Stat StairBrake;

        public Stat LadderSpeed;
        public Stat LadderAccel;
        public Stat LadderBrake;
    }
}