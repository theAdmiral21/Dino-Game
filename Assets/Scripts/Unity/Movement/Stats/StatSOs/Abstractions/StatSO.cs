using System;
using Movement.Core.Stats;
using UnityEngine;

namespace Movement.Unity.Stats.StatSOs.Abstractions
{
    public abstract class StatSO : ScriptableObject, IGameStat
    {
        public abstract Type StatType { get; }

        public abstract object BuildRunTime();
    }
}