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
    public class DashStatSO : StatSO
    {
        [Header("Jump Stats")]
        public override Type StatType => typeof(IJumpStats);

        public int TotalDashes => _totalDashes;
        [SerializeField] int _totalDashes;

        public float DashDist => _dashDist;
        [SerializeField] float _dashDist;

        public float DashTime => _dashTime;
        [SerializeField] float _dashTime;

        public float DashBufferTime => _dashBufferTime;
        [SerializeField] float _dashBufferTime;

        public override object BuildRunTime()
        {
            return new DashStats
            {
                TotalDashes = new Stat(TotalDashes),
                DashDist = new Stat(DashDist),
                DashTime = new Stat(DashTime),
                DashBufferTime = new Stat(DashBufferTime),
            };
        }
    }
}