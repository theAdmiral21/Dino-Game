namespace Primitives.Detection
{
    public struct DetectorStats
    {
        public readonly float AudioAcuity;
        public readonly float VisualAcuity;
        public readonly float SightDistance;
        public readonly float NightVision;
        public readonly float OlfactoryAcuity;

        public DetectorStats(
                                float audioAcuity,
                                float visualAcuity,
                                float sightDistance,
                                float nightVision,
                                float olfactoryAcuity
                            )
        {
            AudioAcuity = audioAcuity;
            VisualAcuity = visualAcuity;
            SightDistance = sightDistance;
            NightVision = nightVision;
            OlfactoryAcuity = olfactoryAcuity;
        }
    }
}