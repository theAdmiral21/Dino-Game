using Primitives.Physics;
using UnityEngine;
using System.Collections.Generic;
using Movement.Unity.Stats;
using Editor.DesignTools.Enums;
using Movement.Unity.Stats.StatSOs.Abstractions;

namespace Tools.DesignTools
{
    public class VisualizeJumpChain : DesignToolsBase
    {
        [System.Serializable]
        public class JumpSegment
        {
            [Header("Stat Source")]
            public StatEnum StatType;

            [Header("Kinematic Overrides")]
            [Tooltip("Only used when no stat object is resolved for this segment.")]
            public float HorizontalVelocity = 5f;

            [Header("Visuals")]
            public Color TrajectoryColor = Color.cyan;
        }

        [Header("Stat Objects")]
        public JumpStatSO JumpStats;
        public WallStatSO WallStats;
        public RunStatSO RunStats;
        public SprintStatSO SprintStats;
        public AerialStatSO AerialStats;
        public DoubleJumpStatSO DoubleJumpStats;
        public GravityStatSO GravityStats;
        public LongJumpStatSO LongJumpStats;

        [Header("Jump Chain")]
        public List<JumpSegment> Jumps = new List<JumpSegment>();

        [Header("Simulation")]
        [Tooltip("Number of fixed-timestep steps to simulate per segment.")]
        public int Steps = 60;

        [Header("Display")]
        public bool ShowLabels = true;
        public bool ShowLandingMarkers = true;
        public bool ShowApexMarkers = true;
        public Color LandingColor = Color.yellow;
        public Color ApexColor = Color.green;

        private float _dt => Time.fixedDeltaTime;
        private Vector3 _origin => transform.position;

        private void OnDrawGizmos()
        {
            if (Jumps == null || Jumps.Count == 0) return;

            Vector3 segmentOrigin = _origin;
            KinematicResult kinematic = new();
            for (int i = 0; i < Jumps.Count; i++)
            {
                var segment = Jumps[i];
                kinematic = ComputeKinematics(segment, ref kinematic);
                segmentOrigin = DrawSegment(segment, kinematic, segmentOrigin, i);
            }
        }

        /// <summary>
        /// Resolves the stat SO for this segment via its StatType enum, reads jump
        /// height and apex time from it, then falls back to the segment's manual
        /// override values if the SO slot is empty or the stat is missing.
        /// </summary>
        private KinematicResult ComputeKinematics(JumpSegment segment, ref KinematicResult kinematic)
        {
            float horizontalVelocity = segment.HorizontalVelocity;

            kinematic.Velocity.x = horizontalVelocity;

            if (segment.StatType == StatEnum.Jump)
            {
                kinematic = CalcJump(ref kinematic);
            }
            else if (segment.StatType == StatEnum.Gravity)
            {
                kinematic = CalcFall(ref kinematic);
            }

            return kinematic;
        }

        private KinematicResult CalcJump(ref KinematicResult kinematicResult)
        {
            // Calculate the jump variables
            float gravity = -2 * JumpStats.JumpHeight / Mathf.Pow(JumpStats.JumpApexTime, 2);
            kinematicResult.Gravity = gravity;
            kinematicResult.Velocity.y = Mathf.Abs(gravity) * JumpStats.JumpApexTime;

            return kinematicResult;
        }

        private KinematicResult CalcFall(ref KinematicResult kinematicResult)
        {
            kinematicResult.Gravity = GravityStats.BaseGravity * GravityStats.SlowFall;


            return kinematicResult;
        }

        /// <summary>
        /// Maps a StatEnum to the corresponding SO field on this component.
        /// The SOs implement IStatSheet directly, so no adapter is needed.
        /// Add new cases here as you introduce more stat types.
        /// </summary>
        private StatSO GetStatValues(StatEnum statType)
        {
            return statType switch
            {
                StatEnum.Jump => JumpStats,
                StatEnum.WallJump => WallStats,
                StatEnum.Run => RunStats,
                StatEnum.Sprint => SprintStats,
                StatEnum.Aerial => AerialStats,
                StatEnum.DoubleJump => DoubleJumpStats,
                StatEnum.Gravity => GravityStats,
                StatEnum.LongJump => LongJumpStats,
                _ => null
            };
        }

        private Vector3 DrawSegment(JumpSegment segment, KinematicResult kinematic, Vector3 origin, int index)
        {
            Vector3 prev = origin;
            Vector3 landing = origin;

            float theoreticalApexT = kinematic.Velocity.y / Mathf.Abs(kinematic.Gravity);
            bool apexDrawn = false;

            for (int i = 1; i <= Steps; i++)
            {
                float t = i * _dt;
                Vector3 point = SampleTrajectory(origin, t, kinematic);

                Gizmos.color = segment.TrajectoryColor;
                Gizmos.DrawLine(prev, point);

                prev = point;
                landing = point;

                if (ShowApexMarkers && !apexDrawn && t >= theoreticalApexT)
                {
                    Vector3 apexPoint = SampleTrajectory(origin, theoreticalApexT, kinematic);
                    Gizmos.color = ApexColor;
                    Gizmos.DrawSphere(apexPoint, 0.08f);
                    apexDrawn = true;
                }
            }

            if (ShowLandingMarkers)
            {
                Gizmos.color = LandingColor;
                Gizmos.DrawSphere(landing, 0.12f);
                Gizmos.DrawLine(landing, landing + Vector3.up * 0.3f);
            }

#if UNITY_EDITOR
            if (ShowLabels)
            {
                string label = $"[{index + 1}] {segment.StatType}";
                UnityEditor.Handles.color = segment.TrajectoryColor;
                UnityEditor.Handles.Label(origin + Vector3.up * 0.25f, label);
            }
#endif

            return landing;
        }

        private Vector3 SampleTrajectory(Vector3 origin, float t, KinematicResult kinematic)
        {


            float x = origin.x + kinematic.Velocity.x * t;
            float y = origin.y + kinematic.Velocity.y * t + 0.5f * kinematic.Gravity * t * t;
            // if (y < 0)
            // {
            //     Debug.Log($"Calc'd fall");
            //     kinematic = CalcFall(ref kinematic);
            // }
            Debug.Log($"gravity at time {t}: {kinematic.Gravity}, y vel: {y}");
            return new Vector3(x, y, origin.z);
        }
    }
}