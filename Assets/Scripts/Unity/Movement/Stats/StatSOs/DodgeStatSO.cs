using UnityEngine;
using System;
using Movement.Core.Stats;
using Movement.Unity.Stats.StatSOs.Abstractions;
using Primitives.Stats;
using Primitives.Stats.DataStructures;

namespace Movement.Unity.Stats
{
    [CreateAssetMenu(fileName = "DashStats", menuName = "Stats/Dash Stats")]

    [Serializable]
    public class DodgeStatSO : StatSO
    {
        [Header("Dodge Stats")]
        public override Type StatType => typeof(IJumpStats);

        public int TotalDodges => _totalDodges;
        [SerializeField] int _totalDodges;

        public float DodgeDist => _dodgeDist;
        [SerializeField] float _dodgeDist;

        public float DodgeTime => _dodgeTime;
        [SerializeField] float _dodgeTime;

        public float DodgeBufferTime => _dodgeBufferTime;
        [SerializeField] float _dodgeBufferTime;

        public override object BuildRunTime()
        {
            return new DodgeStats
            {
                TotalDodges = new Stat(TotalDodges),
                DodgeDist = new Stat(DodgeDist),
                DodgeTime = new Stat(DodgeTime),
                DodgeBufferTime = new Stat(DodgeBufferTime),
            };
        }
    }
}