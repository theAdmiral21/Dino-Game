using UnityEngine;
using System;
using Movement.Core.Stats;
using Movement.Unity.Stats.StatSOs.Abstractions;
using Primitives.Stats.DataStructures;

namespace Movement.Unity.Stats
{
    [CreateAssetMenu(fileName = "AerialStats", menuName = "Stats/Aerial Stats")]

    [Serializable]
    public class AerialStatSO : StatSO
    {
        [Header("Aerial Stats")]
        public override Type StatType => typeof(IAerialStats);

        public float AerialAccel => _aerialAccel;
        [SerializeField] float _aerialAccel;

        public float AerialBrake => _aerialBrake;
        [SerializeField] float _aerialBrake;

        public override object BuildRunTime()
        {
            return new AerialStats
            {
                AerialAccel = new(AerialAccel),
                AerialBrake = new(AerialBrake),
            };
        }
    }
}