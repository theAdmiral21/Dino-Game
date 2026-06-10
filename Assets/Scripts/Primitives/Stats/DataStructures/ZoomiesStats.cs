using System;

namespace Primitives.Stats.DataStructures
{
    [Serializable]
    public struct ZoomiesStats
    {
        public Type RuntimeType => typeof(ZoomiesStats);
        public Stat ZoomSpeed;
        public Stat ZoomAccel;
        public Stat ZoomBrakeAccel;
        public Stat ZoomAmountLimit;
        public Stat ZoomThreshold;
    }
}