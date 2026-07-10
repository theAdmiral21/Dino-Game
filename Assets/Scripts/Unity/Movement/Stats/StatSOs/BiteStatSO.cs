using UnityEngine;
using System;
using Primitives.Stats.DataStructures;
using Movement.Unity.Stats.StatSOs.Abstractions;
using Primitives.Stats;
using Movement.Core.Stats;

namespace Movement.Unity.Stats
{
    [CreateAssetMenu(fileName = "BiteStatSO", menuName = "Stats/Bite Stats")]
    [Serializable]
    public class BiteStatSO : StatSO
    {
        [Header("Bite Stats")]
        public override Type StatType => typeof(IBiteStats);

        public float Damage => _damage;
        [SerializeField] float _damage;

        public float CoolDown => _coolDown;
        [SerializeField] float _coolDown;

        public override object BuildRunTime()
        {
            return new BiteStats
            {
                Damage = new Stat(Damage),
                CoolDown = new Stat(CoolDown),
            };
        }
    }
}
