using UnityEngine;
using System;
using Movement.Core.Stats;
using Movement.Unity.Stats.StatSOs.Abstractions;
using Primitives.Stats;
using Primitives.Stats.DataStructures;

namespace Movement.Unity.Stats
{
    [CreateAssetMenu(fileName = "RotateStats", menuName = "Stats/Rotate Stats")]

    [Serializable]
    public class RotateStatSO : StatSO
    {
        [Header("Rotate Stats")]
        public override Type StatType => typeof(IRotateStats);

        public float AngularVelocity => _angularVelocity;
        [SerializeField] float _angularVelocity;

        public float AngularAccel => _angularAccel;
        [SerializeField] float _angularAccel;


        public override object BuildRunTime()
        {
            return new RotateStats
            {
                AngularVelocity = new Stat(AngularVelocity),
                AngularAccel = new Stat(AngularAccel),
            };
        }
    }
}