using Primitives.Detection;
using UnityEngine;

namespace Unity.Detection.Detectors.DataStructures
{
    [CreateAssetMenu(fileName = "DetectorStats", menuName = "Game/Detectors/Detector Stats")]
    public class DetectorStatsSO : ScriptableObject
    {
        public float AudioAcuity;
        public float VisualAcuity;
        public float NightVision;
        public float OlfactoryAcuity;


        public DetectorStats BuildRunTime()
        {
            return new DetectorStats(
                AudioAcuity,
                VisualAcuity,
                NightVision,
                OlfactoryAcuity
            );
        }
    }
}