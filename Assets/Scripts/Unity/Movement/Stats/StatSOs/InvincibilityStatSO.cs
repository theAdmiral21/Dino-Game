using UnityEngine;
using System;
using Movement.Core.Stats;
using Movement.Unity.Stats.StatSOs.Abstractions;
using Primitives.Stats;
using Primitives.Stats.DataStructures;

namespace Movement.Unity.Stats
{
    [CreateAssetMenu(fileName = "InvincibilityStats", menuName = "Stats/Invincibility Stats")]

    [Serializable]
    public class InvincibilityStatSO : StatSO
    {
        [Header("Invincibility Stats")]
        public override Type StatType => typeof(IInvincibilityStats);

        public float InvincibilityDuration => _InvincibilityDuration;
        [SerializeField] float _InvincibilityDuration;

        public override object BuildRunTime()
        {
            return new InvincibilityStats
            {
                InvincibilityDuration = new Stat(InvincibilityDuration),
            };
        }
    }
}