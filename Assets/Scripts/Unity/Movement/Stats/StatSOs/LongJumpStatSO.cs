using UnityEngine;
using System;
using Movement.Core.Stats;
using Movement.Unity.Stats.StatSOs.Abstractions;
using Primitives.Stats;
using Primitives.Stats.DataStructures;

namespace Movement.Unity.Stats
{
    [CreateAssetMenu(fileName = "LongJumpStats", menuName = "Stats/Long Jump Stats")]

    [Serializable]
    public class LongJumpStatSO : StatSO
    {
        [Header("Long Jump Far")]
        public override Type StatType => typeof(ILongJumpStats);

        public float LongJumpFarWindow => _longJumpFarWindow;
        [SerializeField] float _longJumpFarWindow;

        public float LongJumpFarHeight => _longJumpFarHeight;
        [SerializeField] float _longJumpFarHeight;

        public float LongJumpFarApexTime => _longJumpFarApexTime;
        [SerializeField] float _longJumpFarApexTime;

        public float LongJumpFarSpeed => _longJumpFarSpeed;
        [SerializeField] float _longJumpFarSpeed;

        [Header("Long Jump Medium")]

        public float LongJumpMedHeight => _longJumpMedHeight;
        [SerializeField] float _longJumpMedHeight;

        public float LongJumpMedApexTime => _longJumpMedApexTime;
        [SerializeField] float _longJumpMedApexTime;

        public float LongJumpMedSpeed => _longJumpMedSpeed;
        [SerializeField] float _longJumpMedSpeed;

        public override object BuildRunTime()
        {
            return new LongJumpStats
            {
                LongJumpFarWindow = new Stat(LongJumpFarWindow),
                LongJumpFarHeight = new Stat(LongJumpFarHeight),
                LongJumpFarApexTime = new Stat(LongJumpFarApexTime),
                LongJumpFarSpeed = new Stat(LongJumpFarSpeed),
                LongJumpMedHeight = new Stat(LongJumpMedHeight),
                LongJumpMedApexTime = new Stat(LongJumpMedApexTime),
                LongJumpMedSpeed = new Stat(LongJumpMedSpeed),
            };
        }
    }
}