using UnityEngine;

namespace Primitives.Detectors
{
    public interface IDetectionData
    {
        public DetectionReading ReadingQuality { get; }
        public Vector2 ObjectPos { get; }
        public Vector2 ObservationPos { get; }
        public Vector2 Bearing { get; }
    }
}