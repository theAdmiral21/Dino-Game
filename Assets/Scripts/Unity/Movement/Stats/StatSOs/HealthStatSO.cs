using UnityEngine;
using System;
using Primitives.Stats.DataStructures;
using Movement.Unity.Stats.StatSOs.Abstractions;
using Movement.Core.Stats;
using Primitives.Stats;

namespace Movement.Unity.Stats
{
    [CreateAssetMenu(fileName = "HealthStatSO", menuName = "Stats/Health Stats")]
    [Serializable]
    public class HealthStatSO : StatSO
    {
        [Header("Health Stats")]
        public override Type StatType => typeof(IHealthStats);

        public float TotalHealth => _totalHealth;
        [SerializeField] float _totalHealth;

        public override object BuildRunTime()
        {
            return new HealthStats
            {
                TotalHealth = new Stat(TotalHealth),
            };
        }
    }
}
