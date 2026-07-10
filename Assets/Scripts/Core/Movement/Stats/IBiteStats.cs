using System;

namespace Movement.Core.Stats
{
    public interface IBiteStats : IGameStat
    {
        public float Damage { get; }
        public float CoolDown { get; }
    }
}
