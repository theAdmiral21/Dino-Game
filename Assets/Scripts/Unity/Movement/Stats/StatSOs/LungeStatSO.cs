using UnityEngine;
using System;
using Movement.Core.Stats;
using Movement.Unity.Stats.StatSOs.Abstractions;
using Primitives.Stats;
using Primitives.Stats.DataStructures;

namespace Movement.Unity.Stats
{
    [CreateAssetMenu(fileName = "LungeStats", menuName = "Stats/Lunge Stats")]

    [Serializable]
    public class LungeStatSO : StatSO
    {
        [Header("Lunge Stats")]
        public override Type StatType => typeof(ILungeStats);

        public int TotalLunges => _totalLunges;
        [SerializeField] int _totalLunges;

        public int LungeDistance => _lungeDistance;
        [SerializeField] int _lungeDistance;

        public float LungeHeight => _LungeHeight;
        [SerializeField] float _LungeHeight;

        public float LungeApexTime => _LungeApexTime;
        [SerializeField] float _LungeApexTime;

        public float LungeDuration => _lungeDuration;
        [SerializeField] float _lungeDuration;

        public float LungeDamage => _LungeDamage;
        [SerializeField] float _LungeDamage;

        public override object BuildRunTime()
        {
            return new LungeStats
            {
                TotalLunges = new Stat(TotalLunges),
                LungeHeight = new Stat(LungeHeight),
                LungeApexTime = new Stat(LungeApexTime),
                LungeDistance = new Stat(LungeDistance),
                LungeDuration = new Stat(LungeDuration),
                LungeDamage = new Stat(LungeDamage),

            };
        }
    }
}