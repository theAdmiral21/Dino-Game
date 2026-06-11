using UnityEngine;
using System;
using Movement.Core.Stats;
using Movement.Unity.Stats.StatSOs.Abstractions;
using Primitives.Stats;
using Primitives.Stats.DataStructures;
using Movement.Core.Abstractions;

namespace Movement.Unity.Stats
{
    [CreateAssetMenu(fileName = "ZoomieStats", menuName = "Stats/Zoomie Stats")]

    [Serializable]
    public class ZoomiesStatSO : StatSO
    {
        [Header("Zoomies Stats")]
        public override Type StatType => typeof(IZoomiesState);

        public float ZoomSpeed => _ZoomieSpeed;
        [SerializeField] float _ZoomieSpeed;

        public float ZoomAccel => _ZoomieAccel;
        [SerializeField] float _ZoomieAccel;

        public float ZoomBrakeAccel => _ZoomieBrakeAccel;
        [SerializeField] float _ZoomieBrakeAccel;

        public float ZoomAmountLimit => _ZoomAmountLimit;
        [SerializeField] float _ZoomAmountLimit;

        public float ZoomThreshold => _ZoomThreshold;
        [SerializeField] float _ZoomThreshold;

        public override object BuildRunTime()
        {
            return new ZoomiesStats
            {
                ZoomSpeed = new Stat(ZoomSpeed),
                ZoomAccel = new Stat(ZoomAccel),
                ZoomBrakeAccel = new Stat(ZoomBrakeAccel),
                ZoomAmountLimit = new Stat(ZoomAmountLimit),
                ZoomThreshold = new Stat(ZoomThreshold),
            };
        }
    }
}