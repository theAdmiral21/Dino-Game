using UnityEngine;
using System;
using Movement.Core.Stats;
using Movement.Unity.Stats.StatSOs.Abstractions;
using Primitives.Stats;
using Primitives.Stats.DataStructures;

namespace Movement.Unity.Stats
{
    [CreateAssetMenu(fileName = "DoubleJumpStats", menuName = "Stats/Double Jump Stats")]

    [Serializable]
    public class DoubleJumpStatSO : StatSO
    {
        [Header("Double Jump Stats")]
        public override Type StatType => typeof(IDoubleJumpStats);

        public float DoubleJumpHeight => _doubleJumpHeight;
        [SerializeField] float _doubleJumpHeight;

        public float DoubleJumpApexTime => _doubleJumpApexTime;
        [SerializeField] float _doubleJumpApexTime;

        public override object BuildRunTime()
        {
            return new DoubleJumpStats
            {
                DoubleJumpHeight = new Stat(DoubleJumpHeight),
                DoubleJumpApexTime = new Stat(DoubleJumpApexTime),
            };
        }
    }
}