using UnityEngine;
using System;
using Movement.Core.Stats;
using Movement.Unity.Stats.StatSOs.Abstractions;
using Primitives.Stats;
using Primitives.Stats.DataStructures;

namespace Movement.Unity.Stats
{
    [CreateAssetMenu(fileName = "JumpStats", menuName = "Stats/Jump Stats")]

    [Serializable]
    public class JumpStatSO : StatSO
    {
        [Header("Jump Stats")]
        public override Type StatType => typeof(IJumpStats);

        public int TotalJumps => _totalJumps;
        [SerializeField] int _totalJumps;

        public float JumpHeight => _jumpHeight;
        [SerializeField] float _jumpHeight;

        public float JumpApexTime => _jumpApexTime;
        [SerializeField] float _jumpApexTime;

        public float JumpBufferTime => _jumpBufferTime;
        [SerializeField] float _jumpBufferTime;

        public float CoyoteTime => _coyoteTime;
        [SerializeField] float _coyoteTime;

        public override object BuildRunTime()
        {
            return new JumpStats
            {
                TotalJumps = new Stat(TotalJumps),
                JumpHeight = new Stat(JumpHeight),
                JumpApexTime = new Stat(JumpApexTime),
                JumpBufferTime = new Stat(JumpBufferTime),
                CoyoteTime = new Stat(CoyoteTime),
            };
        }
    }
}