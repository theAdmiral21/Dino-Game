using UnityEngine;
using Primitives.Detectors;

namespace Enemy.Application.DataStructures
{
    public class LineOfSiteData : IDetectionData
    {
        public Vector2 ObjectPos { get; private set; }

        public Vector2 ObservationPos { get; private set; }

        public Vector2 Bearing => ObjectPos - ObservationPos;

        public DetectionReading ReadingQuality { get; private set; }

        public LineOfSiteData(DetectionReading readingQuality, Vector2 objectPos, Vector2 observationPos)
        {
            ReadingQuality = readingQuality;
            ObjectPos = objectPos;
            ObservationPos = observationPos;
        }
    }
}