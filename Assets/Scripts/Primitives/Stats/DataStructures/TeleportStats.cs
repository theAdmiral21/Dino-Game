using System;

namespace Primitives.Stats.DataStructures
{
    [Serializable]
    public struct TeleportStats
    {
        public Type RuntimeType => typeof(TeleportStats);

        public Stat TeleportRange;
        public Stat TeleportCoolDown;
    }
}