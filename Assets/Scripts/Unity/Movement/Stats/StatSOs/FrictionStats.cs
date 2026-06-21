using UnityEngine;
using System;
using Movement.Core.Stats;
using Movement.Unity.Stats.StatSOs.Abstractions;
using Primitives.Stats.DataStructures;

namespace Movement.Unity.Stats
{
    [CreateAssetMenu(fileName = "FrictionStats", menuName = "Stats/Friction Stats")]

    [Serializable]
    public class FrictionStatSO : StatSO
    {
        [Header("Friction Stats")]
        public override Type StatType => typeof(IFrictionStats);

        public float FrictionalValue => _frictionalValue;
        [SerializeField] float _frictionalValue;

        public override object BuildRunTime()
        {
            return new FrictionStats
            {
                Friction = new(FrictionalValue),
            };
        }
    }
}