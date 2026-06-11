using UnityEngine;
using System;
using Movement.Core.Stats;
using Movement.Unity.Stats.StatSOs.Abstractions;
using Primitives.Stats;
using Primitives.Stats.DataStructures;

namespace Movement.Unity.Stats
{
    [CreateAssetMenu(fileName = "QuickStepStats", menuName = "Stats/Quick Step Stats")]

    [Serializable]
    public class QuickStepStatSO : StatSO
    {
        [Header("Quick Step Stats")]
        public override Type StatType => typeof(IQuickStepStats);

        public float QuickStepDistance => _QuickStepDistance;
        [SerializeField] float _QuickStepDistance;

        public float QuickStepDuration => _QuickStepDuration;
        [SerializeField] float _QuickStepDuration;

        public float QuickStepCoolDown => _QuickStepCoolDown;
        [SerializeField] float _QuickStepCoolDown;

        public override object BuildRunTime()
        {
            return new QuickStepStats
            {
                QuickStepDistance = new Stat(QuickStepDistance),
                QuickStepDuration = new Stat(QuickStepDuration),
                QuickStepCoolDown = new Stat(QuickStepCoolDown),
            };
        }
    }
}