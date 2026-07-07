using UnityEngine;
using System;
using Movement.Core.Stats;
using Movement.Unity.Stats.StatSOs.Abstractions;
using Primitives.Stats;
using Primitives.Stats.DataStructures;

namespace Movement.Unity.Stats
{
    [CreateAssetMenu(fileName = "ClimbStats", menuName = "Stats/Climb Stats")]

    [Serializable]
    public class ClimbStatSO : StatSO
    {
        [Header("Climb Stats")]
        public override Type StatType => typeof(IClimbStats);

        public float StairSpeed => _StairSpeed;
        [SerializeField] float _StairSpeed;
        public float StairAccel => _StairAccel;
        [SerializeField] float _StairAccel;
        public float StairBrake => _StairBrake;
        [SerializeField] float _StairBrake;

        public float LadderSpeed => _LadderSpeed;
        [SerializeField] float _LadderSpeed;
        public float LadderAccel => _LadderAccel;
        [SerializeField] float _LadderAccel;
        public float LadderBrake => _LadderBrake;
        [SerializeField] float _LadderBrake;

        public override object BuildRunTime()
        {
            return new ClimbStats
            {
                StairSpeed = new Stat(StairSpeed),
                StairAccel = new Stat(StairAccel),
                StairBrake = new Stat(StairBrake),
                LadderSpeed = new Stat(LadderSpeed),
                LadderAccel = new Stat(LadderAccel),
                LadderBrake = new Stat(LadderBrake),
            };
        }
    }
}
