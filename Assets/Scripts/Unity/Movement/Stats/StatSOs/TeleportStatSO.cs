using UnityEngine;
using System;
using Movement.Core.Stats;
using Movement.Unity.Stats.StatSOs.Abstractions;
using Primitives.Stats;
using Primitives.Stats.DataStructures;

namespace Movement.Unity.Stats
{
    [CreateAssetMenu(fileName = "TeleportStats", menuName = "Stats/Teleport Stats")]

    [Serializable]
    public class TeleportStatSO : StatSO
    {
        [Header("Quick Step Stats")]
        public override Type StatType => typeof(ITeleportStats);
        public float TeleportRange => _TeleportRange;
        [SerializeField] float _TeleportRange;

        public float TeleportCoolDown => _TeleportCoolDown;
        [SerializeField] float _TeleportCoolDown;

        public override object BuildRunTime()
        {
            return new TeleportStats
            {
                TeleportRange = new Stat(TeleportRange),
                TeleportCoolDown = new Stat(TeleportCoolDown),
            };
        }
    }
}