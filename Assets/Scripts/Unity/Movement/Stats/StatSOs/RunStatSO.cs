using UnityEngine;
using System;
using Movement.Core.Stats;
using Movement.Unity.Stats.StatSOs.Abstractions;
using Primitives.Stats;
using Primitives.Stats.DataStructures;

namespace Movement.Unity.Stats
{
    [CreateAssetMenu(fileName = "RunStats", menuName = "Stats/Run Stats")]

    [Serializable]
    public class RunStatSO : StatSO
    {
        [Header("Run Stats")]
        public override Type StatType => typeof(IRunStats);

        public float RunSpeed => _runSpeed;
        [SerializeField] float _runSpeed;

        public float RunAccel => _runAccel;
        [SerializeField] float _runAccel;

        public float BrakeAccel => _brakeAccel;
        [SerializeField] float _brakeAccel;

        public float CrouchWalkSpeed => _crouchWalkSpeed;
        [SerializeField] float _crouchWalkSpeed;

        public float CrouchWalkAccel => _crouchWalkAccel;
        [SerializeField] float _crouchWalkAccel;

        public float CrouchWalkBrake => _crouchWalkBrake;
        [SerializeField] float _crouchWalkBrake;



        public override object BuildRunTime()
        {
            return new RunStats
            {
                RunSpeed = new Stat(RunSpeed),
                RunAccel = new Stat(RunAccel),
                BrakeAccel = new Stat(BrakeAccel),
                CrouchWalkSpeed = new Stat(CrouchWalkSpeed),
                CrouchWalkAccel = new Stat(CrouchWalkAccel),
                CrouchWalkBrake = new Stat(CrouchWalkBrake),
            };
        }
    }
}