using UnityEngine;
using System;
using Movement.Core.Stats;
using Movement.Unity.Stats.StatSOs.Abstractions;
using Primitives.Stats;
using Primitives.Stats.DataStructures;

namespace Movement.Unity.Stats
{
    [CreateAssetMenu(fileName = "WallStats", menuName = "Stats/Wall Stats")]

    [Serializable]
    public class WallStatSO : StatSO
    {
        [Header("Wall Stats")]
        public override Type StatType => typeof(IWallStats);
        public float WallSlideSpeed => _WallSlideSpeed;
        [SerializeField] float _WallSlideSpeed;
        public float WallJumpHeight => _WallJumpHeight;
        [SerializeField] float _WallJumpHeight;
        public float WallJumpXDuration => _WallJumpXDuration;
        [SerializeField] float _WallJumpXDuration;
        public float WallJumpApexTime => _WallJumpApexTime;
        [SerializeField] float _WallJumpApexTime;
        public float WallJumpXDistance => _WallJumpXDistance;
        [SerializeField] float _WallJumpXDistance;
        public float WallJumpBufferTime => _WallJumpBufferTime;
        [SerializeField] float _WallJumpBufferTime;

        public override object BuildRunTime()
        {
            return new WallStats
            {
                WallSlideSpeed = new Stat(WallSlideSpeed),
                WallJumpHeight = new Stat(WallJumpHeight),
                WallJumpXDuration = new Stat(WallJumpXDuration),
                WallJumpApexTime = new Stat(WallJumpApexTime),
                WallJumpXDistance = new Stat(WallJumpXDistance),
                WallJumpBufferTime = new Stat(WallJumpBufferTime),
            };
        }
    }
}