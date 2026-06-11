using UnityEngine;
using System;
using Movement.Core.Stats;
using Movement.Unity.Stats.StatSOs.Abstractions;
using Primitives.Stats;
using Primitives.Stats.DataStructures;

namespace Movement.Unity.Stats
{
    [CreateAssetMenu(fileName = "SprintStats", menuName = "Stats/Sprint Stats")]

    [Serializable]
    public class SprintStatSO : StatSO
    {
        [Header("Sprint Stats")]
        public override Type StatType => typeof(ISprintStats);
        public float SprintSpeed => _sprintSpeed;
        [SerializeField] float _sprintSpeed;

        public float SprintAccel => _sprintAccel;
        [SerializeField] float _sprintAccel;

        public override object BuildRunTime()
        {
            return new SprintStats
            {
                SprintSpeed = new Stat(SprintSpeed),
                SprintAccel = new Stat(SprintAccel),
            };
        }
    }
}