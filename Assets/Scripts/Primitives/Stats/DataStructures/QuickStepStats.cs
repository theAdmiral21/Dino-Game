using System;

namespace Primitives.Stats.DataStructures
{
    [Serializable]
    public struct QuickStepStats
    {
        public Type RuntimeType => typeof(QuickStepStats);
        public Stat QuickStepDistance;
        public Stat QuickStepDuration;
        public Stat QuickStepCoolDown;
    }
}