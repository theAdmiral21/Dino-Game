using UnityEngine;
using System;
using Movement.Core.Stats;
using Movement.Unity.Stats.StatSOs.Abstractions;
using Primitives.Stats;
using Primitives.Stats.DataStructures;

namespace Movement.Unity.Stats
{
    [CreateAssetMenu(fileName = "GravityStats", menuName = "Stats/Gravity Stats")]

    [Serializable]
    public class GravityStatSO : StatSO
    {
        [Header("Gravity Stats")]
        public override Type StatType => typeof(IGravityStats);

        public float BaseGravity => _baseGravity;
        [SerializeField] float _baseGravity;

        public float SlowFall => _slowFall;
        [SerializeField] float _slowFall;

        public float FastFall => _fastFall;
        [SerializeField] float _fastFall;

        public override object BuildRunTime()
        {
            return new GravityStats
            {
                BaseGravity = new Stat(BaseGravity),
                SlowFall = new Stat(SlowFall),
                FastFall = new Stat(FastFall),
            };
        }
    }
}